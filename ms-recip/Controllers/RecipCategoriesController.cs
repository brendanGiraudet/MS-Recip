using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using ms_recip.Models;
using ms_recip.Repositories.RecipCategoriesRepository;

namespace ms_recip.Controllers;

public class RecipCategoriesController(IRecipCategoriesRepository recipCategoriesRepository) : ODataController
{
    private readonly IRecipCategoriesRepository _recipCategoriesRepository = recipCategoriesRepository;

    [HttpPost]
    [Route("RecipCategories/{recipId:guid}")]
    public async Task<IActionResult> PostAsync([FromRoute] Guid recipId, [FromBody] IEnumerable<RecipCategoryModel> items)
    {
        var saveResult = await _recipCategoriesRepository.SaveItemsAsync(items, i => i.RecipId == recipId);

        if (saveResult.IsSuccess) return Ok(saveResult.Value);

        return StatusCode(StatusCodes.Status500InternalServerError, saveResult.Message);
    }

    [EnableQuery]
    public IActionResult Get()
    {
        var getResult = _recipCategoriesRepository.GetItems();

        if (getResult.IsSuccess && getResult.Value is not null) return Ok(getResult.Value);

        if (getResult.IsSuccess && getResult.Value is null) return NotFound();

        return StatusCode(StatusCodes.Status500InternalServerError, getResult.Message);
    }
}
using Microsoft.AspNetCore.Mvc;
using ms_recip.Models;
using ms_recip.Repositories.RecipCategoriesRepository;

namespace ms_recip.Controllers;

[ApiController]
public class RecipCategoriesController(IRecipCategoriesRepository recipCategoriesRepository) : ControllerBase
{
    private readonly IRecipCategoriesRepository _recipCategoriesRepository = recipCategoriesRepository;

    [HttpPost("RecipCategories/{recipId:guid}")]
    public async Task<IActionResult> PostAsync([FromRoute] Guid recipId, [FromBody] IEnumerable<RecipCategoryModel> items)
    {
        var saveResult = await _recipCategoriesRepository.SaveItemsAsync(items, i => i.RecipId == recipId);

        if (saveResult.IsSuccess) return Ok(saveResult.Value);

        return StatusCode(StatusCodes.Status500InternalServerError, saveResult.Message);
    }
}

using Microsoft.AspNetCore.Mvc;
using ms_recip.Models;
using ms_recip.Repositories.IngredientQuantitiesRepository;

namespace ms_recip.Controllers;

[ApiController]
public class IngredientQuantitiesController(IIngredientQuantitiesRepository ingredientQuantitiesRepository) : ControllerBase
{
    private readonly IIngredientQuantitiesRepository _ingredientQuantitiesRepository = ingredientQuantitiesRepository;

    [HttpPost("IngredientQuantities/{recipId:guid}")]
    public async Task<IActionResult> PostAsync([FromRoute] Guid recipId, [FromBody] IEnumerable<IngredientQuantityModel> items)
    {
        var saveResult = await _ingredientQuantitiesRepository.SaveItemsAsync(items, i => i.RecipId == recipId);

        if (saveResult.IsSuccess) return Ok(saveResult.Value);

        return StatusCode(StatusCodes.Status500InternalServerError, saveResult.Message);
    }
}

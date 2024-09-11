using Microsoft.AspNetCore.Mvc;
using ms_recip.Models;
using ms_recip.Repositories.ProfilCategoriesRepository;

namespace ms_recip.Controllers;

[ApiController]
public class ProfilCategoriesController(IProfilCategoriesRepository profilCategoriesRepository) : ControllerBase
{
    private readonly IProfilCategoriesRepository _profilCategoriesRepository = profilCategoriesRepository;

    [HttpPost("ProfilCategories/{profilId:guid}")]
    public async Task<IActionResult> PostAsync([FromRoute] Guid profilId, [FromBody] IEnumerable<ProfilCategoryModel> items)
    {
        var saveResult = await _profilCategoriesRepository.SaveItemsAsync(items, i => i.ProfilId == profilId);

        if (saveResult.IsSuccess) return Ok(saveResult.Value);

        return StatusCode(StatusCodes.Status500InternalServerError, saveResult.Message);
    }
}

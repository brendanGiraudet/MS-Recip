using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using ms_recip.Models;
using ms_recip.Repositories.UsersRepository;

namespace ms_recip.Controllers;

public class UsersController(IUsersRepository usersRepository) : ODataController
{
    private readonly IUsersRepository _usersRepository = usersRepository;

    [EnableQuery]
    public IActionResult Get()
    {
        var getResult = _usersRepository.GetItems();

        if (getResult.IsSuccess) return Ok(getResult.Value);

        return StatusCode(StatusCodes.Status500InternalServerError, getResult.Message);
    }

    [EnableQuery]
    public async Task<IActionResult> GetAsync([FromODataUri] string key)
    {
        var getResult = await _usersRepository.GetItemAsync(k => k.UserId == key);

        if (getResult.IsSuccess && getResult.Value is not null) return Ok(getResult.Value);

        if (getResult.IsSuccess && getResult.Value is null) return NotFound();

        return StatusCode(StatusCodes.Status500InternalServerError, getResult.Message);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] UserModel item)
    {
        var createResult = await _usersRepository.CreateItemAsync(item);

        if (createResult.IsSuccess) return Created(createResult.Value);

        return StatusCode(StatusCodes.Status500InternalServerError, createResult.Message);
    }

    [HttpPatch]
    public async Task<IActionResult> PatchAsync([FromODataUri] string key, [FromBody] UserModel item)
    {
        var updateResult = await _usersRepository.UpdateItemAsync(k => k.UserId == key, item);

        if (updateResult.IsSuccess) return Ok(item);

        return StatusCode(StatusCodes.Status500InternalServerError, updateResult.Message);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync([FromODataUri] string key)
    {
        var deleteResult = await _usersRepository.DeleteItemAsync(k => k.UserId == key);

        if (deleteResult.IsSuccess && deleteResult.Value == true) return Ok();

        if (deleteResult.IsSuccess && deleteResult.Value == false) return NotFound();

        return StatusCode(StatusCodes.Status500InternalServerError, deleteResult.Message);
    }
}

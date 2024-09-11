﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using ms_recip.Models;
using ms_recip.Repositories.RecipCalendarsRepository;

namespace ms_recip.Controllers;

public class RecipCalendarsController(IRecipCalendarsRepository recipCalendarsRepository) : ODataController
{
    private readonly IRecipCalendarsRepository _recipCalendarsRepository = recipCalendarsRepository;

    [EnableQuery]
    public IActionResult Get()
    {
        var getResult = _recipCalendarsRepository.GetItems();

        if (getResult.IsSuccess) return Ok(getResult.Value);

        return StatusCode(StatusCodes.Status500InternalServerError, getResult.Message);
    }

    [EnableQuery]
    public async Task<IActionResult> GetAsync([FromODataUri] Guid key)
    {
        var getResult = await _recipCalendarsRepository.GetItemAsync(k => k.Id == key);

        if (getResult.IsSuccess && getResult.Value is not null) return Ok(getResult.Value);

        if (getResult.IsSuccess && getResult.Value is null) return NotFound();

        return StatusCode(StatusCodes.Status500InternalServerError, getResult.Message);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] RecipCalendarModel item)
    {
        var createResult = await _recipCalendarsRepository.CreateItemAsync(item);

        if (createResult.IsSuccess) return Created(createResult.Value);

        return StatusCode(StatusCodes.Status500InternalServerError, createResult.Message);
    }

    [HttpPatch]
    public async Task<IActionResult> PatchAsync([FromODataUri] Guid key, [FromBody] RecipCalendarModel item)
    {
        var updateResult = await _recipCalendarsRepository.UpdateItemAsync(k => k.Id == key, item);

        if (updateResult.IsSuccess) return Ok(item);

        return StatusCode(StatusCodes.Status500InternalServerError, updateResult.Message);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync([FromODataUri] Guid key)
    {
        var deleteResult = await _recipCalendarsRepository.DeleteItemAsync(k => k.Id == key);

        if (deleteResult.IsSuccess && deleteResult.Value == true) return Ok();

        if (deleteResult.IsSuccess && deleteResult.Value == false) return NotFound();

        return StatusCode(StatusCodes.Status500InternalServerError, deleteResult.Message);
    }
}

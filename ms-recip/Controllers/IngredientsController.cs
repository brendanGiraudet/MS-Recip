﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using ms_recip.Models;
using ms_recip.Repository.IngredientsRepository;

namespace ms_recip.Controllers;

public class IngredientsController(IIngredientsRepository ingredientsRepository) : ODataController
{
    private readonly IIngredientsRepository _ingredientsRepository = ingredientsRepository;

    [EnableQuery]
    public IActionResult Get()
    {
        var getResult = _ingredientsRepository.GetItems();

        if (getResult.IsSuccess) return Ok(getResult.Value);

        return StatusCode(StatusCodes.Status500InternalServerError, getResult.Message);
    }

    [EnableQuery]
    public async Task<IActionResult> GetAsync([FromODataUri] Guid key)
    {
        var getResult = await _ingredientsRepository.GetItemAsync(k => k.Id == key);

        if (getResult.IsSuccess && getResult.Value is not null) return Ok(getResult.Value);

        if (getResult.IsSuccess && getResult.Value is null) return NotFound();

        return StatusCode(StatusCodes.Status500InternalServerError, getResult.Message);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] IngredientModel item)
    {
        var createResult = await _ingredientsRepository.CreateItemAsync(item);

        if (createResult.IsSuccess) return Created(createResult.Value);

        return StatusCode(StatusCodes.Status500InternalServerError, createResult.Message);
    }

    [HttpPatch]
    public async Task<IActionResult> PatchAsync([FromODataUri] Guid key, [FromBody] IngredientModel item)
    {
        var updateResult = await _ingredientsRepository.UpdateItemAsync(k => k.Id == key, item);

        if (updateResult.IsSuccess) return Ok(item);

        return StatusCode(StatusCodes.Status500InternalServerError, updateResult.Message);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync([FromODataUri] Guid key)
    {
        var deleteResult = await _ingredientsRepository.DeleteItemAsync(k => k.Id == key);

        if (deleteResult.IsSuccess && deleteResult.Value == true) return Ok();

        if (deleteResult.IsSuccess && deleteResult.Value == false) return NotFound();

        return StatusCode(StatusCodes.Status500InternalServerError, deleteResult.Message);
    }
}

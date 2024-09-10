using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using ms_recip.Data;
using ms_recip.Models;

namespace ms_recip.Controllers;

public class IngredientsController(DatabaseContext context) : ODataController
{
    private readonly DatabaseContext _context = context;

    [EnableQuery]
    public IActionResult Get()
    {
        return Ok(_context.Ingredients);
    }

    [EnableQuery]
    public IActionResult Get([FromODataUri] Guid key)
    {
        var config = _context.Ingredients.FirstOrDefault(c => c.Id == key);
        if (config == null)
        {
            return NotFound();
        }
        return Ok(config);
    }

    [HttpPost]
    public IActionResult Post([FromBody] IngredientModel config)
    {
        _context.Ingredients.Add(config);
        _context.SaveChanges();

        return Created(config);
    }
    
    [HttpPatch]
    public IActionResult Patch([FromODataUri] Guid key, [FromBody] IngredientModel ingredient)
    {
        var actualIngredient = _context.Ingredients.AsNoTracking().FirstOrDefault(c => c.Id == key);

        if(actualIngredient == null) return NotFound();

        _context.Ingredients.Update(ingredient);
        _context.SaveChanges();

        return Ok(ingredient);
    }
    
    [HttpDelete]
    public IActionResult Delete([FromODataUri] Guid key)
    {
        var actualIngredient = _context.Ingredients.FirstOrDefault(c => c.Id == key);

        if(actualIngredient == null) return NotFound();

        actualIngredient.Deleted = true;

        _context.Ingredients.Update(actualIngredient);
        _context.SaveChanges();

        return Ok(actualIngredient);
    }
}

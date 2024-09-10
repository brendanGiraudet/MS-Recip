using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using ms_recip.Data;
using ms_recip.Models;

namespace ms_recip.Controllers;

public class CategoriesController(DatabaseContext context) : ODataController
{
    private readonly DatabaseContext _context = context;

    [EnableQuery]
    public IActionResult Get()
    {
        return Ok(_context.Categories);
    }

    [EnableQuery]
    public IActionResult Get([FromODataUri] Guid key)
    {
        var config = _context.Categories.FirstOrDefault(c => c.Id == key);
        if (config == null)
        {
            return NotFound();
        }
        return Ok(config);
    }

    [HttpPost]
    public IActionResult Post([FromBody] CategoryModel config)
    {
        _context.Categories.Add(config);
        _context.SaveChanges();

        return Created(config);
    }
    
    [HttpPatch]
    public IActionResult Patch([FromODataUri] Guid key, [FromBody] CategoryModel ingredient)
    {
        var actualIngredient = _context.Categories.AsNoTracking().FirstOrDefault(c => c.Id == key);

        if(actualIngredient == null) return NotFound();

        _context.Categories.Update(ingredient);
        _context.SaveChanges();

        return Ok(ingredient);
    }
    
    [HttpDelete]
    public IActionResult Delete([FromODataUri] Guid key)
    {
        var actualIngredient = _context.Categories.FirstOrDefault(c => c.Id == key);

        if(actualIngredient == null) return NotFound();

        actualIngredient.Deleted = true;

        _context.Categories.Update(actualIngredient);
        _context.SaveChanges();

        return Ok(actualIngredient);
    }
}

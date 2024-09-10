using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using ms_recip.Data;
using ms_recip.Models;

namespace ms_recip.Controllers;

public class RecipsController(DatabaseContext context) : ODataController
{
    private readonly DatabaseContext _context = context;

    [EnableQuery]
    public IActionResult Get()
    {
        return Ok(_context.Recips);
    }

    [EnableQuery]
    public IActionResult Get([FromODataUri] Guid key)
    {
        var config = _context.Recips.FirstOrDefault(c => c.Id == key);
        if (config == null)
        {
            return NotFound();
        }
        return Ok(config);
    }

    [HttpPost]
    public IActionResult Post([FromBody] RecipModel config)
    {
        _context.Recips.Add(config);
        _context.SaveChanges();

        return Created(config);
    }
}

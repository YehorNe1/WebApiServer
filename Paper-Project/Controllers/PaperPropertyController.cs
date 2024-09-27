using DataAccess;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Paper_Project.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaperPropertyController : ControllerBase
{
    private readonly MyDbContext _context;

    public PaperPropertyController(MyDbContext context)
    {
        _context = context;
    }

    // GET: api/PaperProperty
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Property>>> GetPaperProperties()
    {
        return await _context.Properties.ToListAsync();
    }

    // GET: api/PaperProperty/5
    [HttpGet("{paperId}/{propertyId}")]
    public async Task<ActionResult<Property>> GetPaperProperty(int paperId, int propertyId)
    {
        var paperProperty = await _context.PaperProperties
            .FirstOrDefaultAsync(pp => pp.PaperId == paperId && pp.PropertyId == propertyId);

        if (paperProperty == null)
        {
            return NotFound();
        }

        return paperProperty;
    }

    // POST: api/PaperProperty
    [HttpPost]
    public async Task<ActionResult<Pro>> PostPaperProperty(PaperProperty paperProperty)
    {
        _context.PaperProperties.Add(paperProperty);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPaperProperty), new { paperId = paperProperty.PaperId, propertyId = paperProperty.PropertyId }, paperProperty);
    }

    // DELETE: api/PaperProperty/5
    [HttpDelete("{paperId}/{propertyId}")]
    public async Task<IActionResult> DeletePaperProperty(int paperId, int propertyId)
    {
        var paperProperty = await _context.PaperProperties
            .FirstOrDefaultAsync(pp => pp.PaperId == paperId && pp.PropertyId == propertyId);

        if (paperProperty == null)
        {
            return NotFound();
        }

        _context.PaperProperties.Remove(paperProperty);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
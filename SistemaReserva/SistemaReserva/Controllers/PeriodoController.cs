using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservaAPI.Data;
using ReservaAPI.Models;

namespace ReservaAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PeriodoController : ControllerBase
{
    private readonly ReservaDbContext _context;

    public PeriodoController(ReservaDbContext context)
    {
        _context = context;
    }

    // GET: api/periodo
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Periodo>>> GetPeriodos()
    {
        return await _context.Periodos.ToListAsync();
    }

    // GET: api/periodo/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Periodo>> GetPeriodo(int id)
    {
        var periodo = await _context.Periodos.FindAsync(id);

        if (periodo == null)
            return NotFound();

        return Ok(periodo);
    }

    // POST: api/periodo
    [HttpPost]
    public async Task<ActionResult<Periodo>> PostPeriodo(Periodo periodo)
    {
        _context.Periodos.Add(periodo);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPeriodo), new { id = periodo.Id }, periodo);
    }

    // PUT: api/periodo/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPeriodo(int id, Periodo periodo)
    {
        if (id != periodo.Id)
            return BadRequest("ID da URL e do corpo não coincidem.");

        _context.Entry(periodo).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Periodos.Any(p => p.Id == id))
                return NotFound();

            throw;
        }

        return NoContent();
    }

    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePeriodo(int id)
    {
        var periodo = await _context.Periodos.FindAsync(id);
        if (periodo == null)
            return NotFound();

        _context.Periodos.Remove(periodo);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}

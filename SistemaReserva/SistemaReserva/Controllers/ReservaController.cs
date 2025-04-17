using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservaAPI.Data;
using ReservaAPI.Dto;
using ReservaAPI.Models;

namespace ReservaAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservaController : ControllerBase
{
    private readonly ReservaDbContext _context;

    public ReservaController(ReservaDbContext context)
    {
        _context = context;
    }

    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Reserva>>> GetReservas()
    {
        return await _context.Reservas
            .Include(r => r.Pessoa)
            .Include(r => r.Sala)
            .Include(r => r.Periodo)
            .ToListAsync();
    }

    
    [HttpGet("{id}")]
    public async Task<ActionResult<Reserva>> GetReserva(int id)
    {
        var reserva = await _context.Reservas
            .Include(r => r.Pessoa)
            .Include(r => r.Sala)
            .Include(r => r.Periodo)
            .FirstOrDefaultAsync(r => r.Id == id);

        if (reserva == null)
            return NotFound();

        return Ok(reserva);
    }


    [HttpPost]
    public async Task<ActionResult<Reserva>> PostReserva(ReservaDTO dto)
    {
        
        bool conflito = await _context.Reservas.AnyAsync(r =>
            r.SalaId == dto.SalaId &&
            r.PeriodoId == dto.PeriodoId &&
            r.Data.Date == DateTime.Now.Date);

        if (conflito)
        {
            return Conflict("Já existe uma reserva para essa sala e período hoje.");
        }

        var reserva = new Reserva
        {
            PessoaId = dto.PessoaId,
            SalaId = dto.SalaId,
            PeriodoId = dto.PeriodoId,
            Data = DateTime.Now
        };

        _context.Reservas.Add(reserva);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetReserva), new { id = reserva.Id }, reserva);
    }




    [HttpPut("{id}")]
    public async Task<IActionResult> PutReserva(int id, Reserva reserva)
    {
        if (id != reserva.Id)
            return BadRequest("ID da URL e do corpo não coincidem.");

        _context.Entry(reserva).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Reservas.Any(r => r.Id == id))
                return NotFound();

            throw;
        }

        return NoContent();
    }

    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReserva(int id)
    {
        var reserva = await _context.Reservas.FindAsync(id);
        if (reserva == null)
            return NotFound();

        _context.Reservas.Remove(reserva);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}

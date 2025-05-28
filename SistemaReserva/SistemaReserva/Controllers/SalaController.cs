using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservaAPI.Data;
using ReservaAPI.DTOs;
using ReservaAPI.Models;

namespace ReservaAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class SalaController : ControllerBase
    {
        private readonly ReservaDbContext _context;
        public SalaController(ReservaDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sala>>> GetSalas()
        {
            return await _context.Salas.ToListAsync();
        }
        [HttpGet("id/{id}")]
        public async Task<ActionResult<Sala>> GetSala(int id)
        {
            var sala = await _context.Salas.FindAsync(id);
            if (sala == null) return NotFound();
            return sala;
        }
        [HttpGet("nome/{name}")]
        public async Task<ActionResult<Sala>> GetSalaNome(string name)
        {
            var sala = await _context.Salas.FirstOrDefaultAsync(s => s.Nome == name);
            if (sala == null) return NotFound();
            return Ok(sala);
        }
        [HttpPost]
        public async Task<ActionResult<Sala>> PostSala(SalaDTO salaDto)
        {
            var sala = new Sala
            {
                Nome = salaDto.Nome,
                LocalizacaoId = salaDto.LocalizacaoId
            };

            _context.Salas.Add(sala);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SalaExists(sala.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction(nameof(GetSala), new { id = sala.Id }, sala);
        }



        [HttpPut("{id}")]
        public async Task<ActionResult<Sala>> PutSala(int id, Sala sala) { 
            var salaExistente = await _context.Salas.FindAsync(id);
            if (salaExistente == null) return NotFound();
            if (sala.Id != id) return BadRequest("ID da sala não corresponde ao ID fornecido.");
          
            salaExistente.Localizacao = sala.Localizacao;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return Conflict();
                    throw;
                }
            }
            return CreatedAtAction(nameof(GetSala), new { id = sala.Id }, sala);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSala(int id)
        {
            var sala = await _context.Salas.FindAsync(id);
            if (sala == null) return NotFound();
            _context.Salas.Remove(sala);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        private bool SalaExists(int id)
        {
            return _context.Salas.Any(e => e.Id == id);
        }
    }
    
}

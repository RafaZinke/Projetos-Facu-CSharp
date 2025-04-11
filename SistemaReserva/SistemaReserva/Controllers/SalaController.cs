using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservaAPI.Data;
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
        public async Task<ActionResult<Sala>> PostSala(Sala sala)
        {
            if (sala.Id <= 0)
            {
                return BadRequest("Sala não pode ter ID negativo.");
            }
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
        private bool SalaExists(int id)
        {
            return _context.Salas.Any(e => e.Id == id);
        }
    }
    
}

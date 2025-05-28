using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservaAPI.Data;
using ReservaAPI.Models;

namespace ReservaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocalizacaoController : ControllerBase
    {
        private readonly ReservaDbContext _context;

        public LocalizacaoController(ReservaDbContext context)
        {
            _context = context;
        }

        // GET: api/Localizacao
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Localizacao>>> GetLocalizacoes()
        {
            return await _context.Localizacoes.ToListAsync();
        }

        // GET: api/Localizacao/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Localizacao>> GetLocalizacao(int id)
        {
            var localizacao = await _context.Localizacoes.FindAsync(id);

            if (localizacao == null)
            {
                return NotFound();
            }

            return localizacao;
        }

        // POST: api/Localizacao
        [HttpPost]
        public async Task<ActionResult<Localizacao>> PostLocalizacao(Localizacao localizacao)
        {
            _context.Localizacoes.Add(localizacao);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLocalizacao), new { id = localizacao.Id }, localizacao);
        }

        // PUT: api/Localizacao/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLocalizacao(int id, Localizacao localizacao)
        {
            if (id != localizacao.Id)
            {
                return BadRequest();
            }

            _context.Entry(localizacao).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocalizacaoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Localizacao/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocalizacao(int id)
        {
            var localizacao = await _context.Localizacoes.FindAsync(id);
            if (localizacao == null)
            {
                return NotFound();
            }

            _context.Localizacoes.Remove(localizacao);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LocalizacaoExists(int id)
        {
            return _context.Localizacoes.Any(e => e.Id == id);
        }
    }
}

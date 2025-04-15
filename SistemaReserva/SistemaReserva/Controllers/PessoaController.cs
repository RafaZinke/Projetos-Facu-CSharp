using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservaAPI.Data;
using ReservaAPI.Models;

namespace ReservaAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PessoaController : ControllerBase
{
    private readonly ReservaDbContext _context;

    public PessoaController(ReservaDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Pessoa>>> GetPessoas()
    {
        return await _context.Pessoas.ToListAsync();
    }

    [HttpGet("id/{id}")]
    public async Task<ActionResult<Pessoa>> GetPessoa(int id)
    {
        var pessoa = await _context.Pessoas.FindAsync(id);
        if (pessoa == null) return NotFound();
        return pessoa;
    }

    [HttpGet("nome/{name}")]
    public async Task<ActionResult<Pessoa>> GetPessoaNome(string name)
    {
        var pessoa = await _context.Pessoas.FirstOrDefaultAsync(p => p.Nome == name);
        if (pessoa == null) return NotFound();
        return Ok(pessoa);
    }

    [HttpGet("email/{email}")]
    public async Task<ActionResult<Pessoa>> GetPessoaEmail(string email)
    {
        var pessoa = await _context.Pessoas.FirstOrDefaultAsync(p => p.Email == email);
        if (pessoa == null) return NotFound();
        return Ok(pessoa);
    }

    [HttpPost]
    public async Task<ActionResult<Pessoa>> PostPessoa(Pessoa pessoa)
    {
        if (pessoa.Id <= 0)
        {
            return BadRequest("Pessoa não pode ter ID negativo.");
        }

        _context.Pessoas.Add(pessoa);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            if (_context.Pessoas.Any(e => e.Id == pessoa.Id))
            {
                return Conflict(); 
            }
            else
            {
                
                throw; 
            }

        }

        return CreatedAtAction(nameof(GetPessoa), new { id = pessoa.Id }, pessoa);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutPessoa(int id, Pessoa pessoa)
    {
        if (id != pessoa.Id) return BadRequest();
        if (pessoa == null) return NotFound(); //Adicionado para evitar NullReferenceException
        _context.Entry(pessoa).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePessoa(int id)
    {
        var pessoa = await _context.Pessoas.FindAsync(id);
        if (pessoa == null) return NotFound();
        _context.Pessoas.Remove(pessoa);
        await _context.SaveChangesAsync();
        return NoContent();
    }

  
}

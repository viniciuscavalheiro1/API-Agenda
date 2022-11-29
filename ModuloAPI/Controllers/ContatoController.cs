using Microsoft.AspNetCore.Mvc;
using ModuloAPI.Context;
using ModuloAPI.Entities;

namespace ModuloAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ContatoController : ControllerBase
{
    private readonly AgendaContext _context;
    public ContatoController(AgendaContext context)
    {
        _context = context;
    }
    
    [HttpPost]
    public IActionResult Create(Contato contato)
    {
        _context.Add(contato);
        _context.SaveChanges();
        return CreatedAtAction(nameof(BuscarPorId), new { Id = contato.Id }, contato);
    }

    [HttpGet("{id}")]
    public IActionResult BuscarPorId(Guid id)
    {
        var contato = _context.Contatos.Find(id);
        if (contato == null)
        {
            return NotFound();
        }
        return Ok(contato);
    }
    
        
    [HttpGet("ObterPorNome")]
    public IActionResult BuscarPorNome(string nome)
    {
        var contatos = _context.Contatos.Where(x => x.Nome.Contains((nome)));
        if (contatos == null)
        {
            return NotFound();
        }
        return Ok(contatos);
    }

    [HttpPut("{id}")]
    public IActionResult Atualizar(Guid id, Contato contato)
    {
        var contatoBanco = _context.Contatos.Find(id);

        if (contatoBanco == null)
        {
            return NotFound();
        }

        contatoBanco.Nome = contato.Nome;
        contatoBanco.Telefone = contato.Telefone;
        contatoBanco.Ativo = contato.Ativo;

        _context.Contatos.Update(contatoBanco);
        _context.SaveChanges();

        return Ok(contatoBanco);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        var contatoBanco = _context.Contatos.Find(id);

        if (contatoBanco == null)
        {
            return NotFound();
        }

        _context.Contatos.Remove(contatoBanco);
        _context.SaveChanges();
        return NoContent();
    }
}
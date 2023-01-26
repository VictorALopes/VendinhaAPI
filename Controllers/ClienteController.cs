using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vendinha.Data;
using Vendinha.Models;
using Vendinha.ViewModels;

namespace Vendinha.Controllers;

[ApiController]
[Route("[controller]")]
public class ClienteController : ControllerBase
{
    // [HttpGet(Name = "GetCliente")]
    // [Route("teste")]
    // public List<Cliente> Get()
    // {
    //     return new List<Cliente>();
    // }

    [HttpGet(Name = "GetCliente")]
    public async Task<IActionResult> Get([FromServices] AppDbContext context)
    {
        List<Cliente> clientes = await context
            .Clientes
            .AsNoTracking()
            .ToListAsync();
        return Ok(clientes);
    }

    [HttpPost(Name = "PostCliente")]
    public async Task<IActionResult> Post(
            [FromServices] AppDbContext context
            ,[FromBody] CreateClienteViewModel model
        )
    {
        if (!ModelState.IsValid)
            return BadRequest();

        Cliente cliente = new Cliente
        {
            Nome = "joao"
            ,CPF = "123"
            ,DataNascimento = DateTime.Now
            ,Idade = 12
            ,Email = "aaa"
        };

        try
        {
            await context.Clientes.AddAsync(cliente);
            await context.SaveChangesAsync();
            return Created($"v1/clientes/{cliente.CPF}", cliente);
        }
        catch (System.Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError); 
        }
    }
}

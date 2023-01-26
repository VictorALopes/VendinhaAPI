using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vendinha.Data;
using Vendinha.Models;
using Vendinha.ViewModels;
using Vendinha.Utilities;

namespace Vendinha.Controllers;

[ApiController]
[Route("[controller]")]
public class ClienteController : ControllerBase
{
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAsync([FromServices] AppDbContext context)
    {
        List<Cliente> clientes = await context
            .Clientes
            .AsNoTracking()
            .ToListAsync();
        return Ok(clientes);
    }

    [HttpGet("GetByCPF{CPF}")]
    public async Task<IActionResult> GetByCPFAsync([FromServices] AppDbContext context, [FromRoute] string CPF)
    {
        Cliente cliente = await context
            .Clientes
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.CPF == CPF);
        return cliente == null ? NotFound() : Ok(cliente);
    }

    [HttpPost("Post")]
    public async Task<IActionResult> Post(
            [FromServices] AppDbContext context
            ,[FromBody] CreateClienteViewModel model
        )
    {
        if (!ModelState.IsValid)
            return BadRequest();

        Cliente cliente = new Cliente
        {
            nome = model.nome
            ,CPF = model.CPF
            ,dataNascimento = model.dataNascimento
            ,idade = Util.CalculateAge(model.dataNascimento)
            ,email = string.IsNullOrEmpty(model.email) ? String.Empty : model.email 
        };

        try
        {
            await context.Clientes.AddAsync(cliente);
            await context.SaveChangesAsync();
            return Created($"clientes/{cliente.CPF}", cliente);
        }
        catch (System.Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError); 
        }
    }

}

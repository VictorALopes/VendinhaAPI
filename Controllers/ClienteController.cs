using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vendinha.Data;
using Vendinha.Models;
using Vendinha.ViewModels.Cliente;
using Vendinha.Utilities;

namespace Vendinha.Controllers;

[ApiController]
[Route("[controller]")]
public class ClienteController : ControllerBase
{
    [HttpGet("GetAll")]
    public async Task<ActionResult<List<Cliente>>> GetAsync(
        [FromServices] AppDbContext context)
    {
        List<Cliente> clientes = await context
            .Clientes
            .AsNoTracking()
            .ToListAsync();
        return Ok(clientes);
    }

    [HttpGet("GetByCPF{CPF}")]
    public async Task<ActionResult<List<Cliente>>> GetByCPFAsync(
        [FromServices] AppDbContext context
        ,[FromRoute] string CPF)
    {
        Cliente cliente = await context
            .Clientes
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.CPF == CPF);
        return cliente == null ? NotFound() : Ok(cliente);
    }

    [HttpPost("Post")]
    public async Task<ActionResult<List<Cliente>>> Post(
            [FromServices] AppDbContext context
            ,[FromBody] PostViewModel model
        )
    {
        if (!Util.IsCPFValid(model.CPF))
            ModelState.AddModelError("CPF", "O CPF informado é inválido.");

        if (!string.IsNullOrEmpty(model.email))
        {
            if (!Util.IsEmailValid(model.email))
                ModelState.AddModelError("email", "O email informado é inválido.");
        }
        
        if (!ModelState.IsValid)
            return BadRequest(Util.GetErrorMessages(ModelState));
        

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
        catch (System.Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message); 
        }
    }

    [HttpPut("Put/{CPF}")]
    public async Task<ActionResult<List<Cliente>>> Put(
            [FromServices] AppDbContext context
            ,[FromBody] PutViewModel model
            ,[FromRoute] string CPF
        )
    {
        if (!string.IsNullOrEmpty(model.email))
        {
            if (!Util.IsEmailValid(model.email))
                ModelState.AddModelError("email", "O email informado é inválido.");
        }
        
        if (!ModelState.IsValid)
            return BadRequest(Util.GetErrorMessages(ModelState));

        Cliente cliente = await context
            .Clientes
            .FirstOrDefaultAsync(x => x.CPF == CPF);
        
        if (cliente == null)
            return NotFound();

        try
        {
            cliente.nome = model.nome;
            cliente.dataNascimento = model.dataNascimento;
            cliente.idade = Util.CalculateAge(model.dataNascimento);
            cliente.email = string.IsNullOrEmpty(model.email) ? String.Empty : model.email;

            context.Clientes.Update(cliente);
            await context.SaveChangesAsync();
            return Ok(cliente);
        }
        catch (System.Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message); 
        }
    }

    [HttpDelete("Delete/{CPF}")]
    public async Task<ActionResult<List<Cliente>>> Delete(
            [FromServices] AppDbContext context
            ,[FromRoute] string CPF
        )
    {
        Cliente cliente = await context
            .Clientes
            .FirstOrDefaultAsync(x => x.CPF == CPF);

        try
        {
            context.Clientes.Remove(cliente);
            await context.SaveChangesAsync();
            return Ok($"Cliente deletado. CPF: {CPF}");
        }
        catch (System.Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message); 
        }
    }    
}

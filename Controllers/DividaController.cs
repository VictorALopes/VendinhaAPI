using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vendinha.Data;
using Vendinha.Models;
using Vendinha.ViewModels.Divida;
using static Vendinha.Helpers.Divida.DividaHelper;

namespace Vendinha.Controllers;

[ApiController]
[Route("[controller]")]
public class DividaController : ControllerBase
{
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAsync([FromServices] AppDbContext context)
    {
        List<Divida> dividas = await context
            .Dividas
            .Include(x=>x.clientes)
            .AsNoTracking()
            .ToListAsync();
        return Ok(dividas);
    }

    [HttpGet("GetById{id}")]
    public async Task<IActionResult> GetByIdAsync([FromServices] AppDbContext context, [FromRoute] int id)
    {
        Divida divida = await context
            .Dividas
            .Include(x=>x.clientes)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.id == id);
        return divida == null ? NotFound(Mensagens.DividaNaoEncontrada) : Ok(divida);
    }

    [HttpGet("GetByCPF{CPF}")]
    public async Task<IActionResult> GetByCPFAsync([FromServices] AppDbContext context, [FromRoute] string CPF)
    {
        Task<List<Divida>> dividas = context
            .Dividas
            .Include(x=>x.clientes)
            .AsNoTracking()
            .Where(x => x.CPF ==  CPF)
            .ToListAsync();
        return dividas == null ? NotFound(Mensagens.ClienteNaoTemDividas) : Ok(dividas);
    }

    [HttpPost("Post")]
    public async Task<IActionResult> Post(
            [FromServices] AppDbContext context
            ,[FromBody] PostViewModel model
        )
    {
        if (!ModelState.IsValid)
            return BadRequest();

        if(ClienteTemDividaPendente(model.CPF, context))
            return BadRequest(Mensagens.TemDividaPendente); 
        
        Divida divida = new Divida
        {
            CPF = model.CPF
            ,valor = model.valor
            ,dataCriacao = DateTime.Now
            ,pago = false
            ,dataPagamento = null
            ,clientes = context.Clientes.First( x=> x.CPF == model.CPF )
        };

        try
        {
            await context.Dividas.AddAsync(divida);
            await context.SaveChangesAsync();
            return Created($"dividas/{divida.id}", divida);
        }
        catch (System.Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message); 
        }
    }

    [HttpPut("Put")]
    public async Task<IActionResult> Put(
            [FromServices] AppDbContext context
            ,[FromBody] PutViewModel model
        )
    {
        if (!ModelState.IsValid)
            return BadRequest();

        Divida divida = await context
            .Dividas
            .FirstOrDefaultAsync(x => x.id == model.id);
        
        if (divida == null)
            return NotFound(Mensagens.DividaNaoEncontrada);

        if (divida.pago)
            return UnprocessableEntity(Mensagens.DividaJaFoiPaga);

        if (divida.CPF != model.CPF)
        {
            if(ClienteTemDividaPendente(model.CPF, context))
                return BadRequest(Mensagens.TemDividaPendente); 
        }
        
        try
        {
            divida.valor = model.valor;
            if (model.dataPagamento != null)
            {
                divida.pago = true;
                divida.dataPagamento = model.dataPagamento;
            }
            
            if (divida.CPF != model.CPF)
            {
                divida.CPF = model.CPF;
                divida.clientes = context.Clientes.First(x => x.CPF == model.CPF);
            }

            context.Dividas.Update(divida);
            await context.SaveChangesAsync();
            return Ok(divida);
        }
        catch (System.Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message); 
        }
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> Delete(
            [FromServices] AppDbContext context
            ,[FromRoute] int id
        )
    {
        Divida divida = await context
            .Dividas
            .FirstOrDefaultAsync(x => x.id == id);

        if (divida == null)
            return NotFound(Mensagens.DividaNaoEncontrada);

        if (divida.pago)
            return UnprocessableEntity(Mensagens.DividaJaFoiPaga);

        try
        {
            context.Dividas.Remove(divida);
            await context.SaveChangesAsync();
            return Ok($"Divida excluída. Id: {id}");
        }
        catch (System.Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message); 
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vendinha.Data;
using Vendinha.Models;
using Vendinha.ViewModels.Divida;
using Vendinha.Utilities;

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
            .AsNoTracking()
            .ToListAsync();
        return Ok(dividas);
    }

    [HttpGet("GetById{id}")]
    public async Task<IActionResult> GetByIdAsync([FromServices] AppDbContext context, [FromRoute] int id)
    {
        Divida divida = await context
            .Dividas
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.id == id);
        return divida == null ? NotFound("Uma dívida com este Id não foi encontrada") : Ok(divida);
    }

    [HttpGet("GetByCliente{CPF}")]
    public async Task<IActionResult> GetByCPFAsync([FromServices] AppDbContext context, [FromRoute] string CPF)
    {
        Task<List<Divida>> dividas = context
            .Dividas
            .AsNoTracking()
            .Where(x => x.CPF ==  CPF)
            .ToListAsync();
        return dividas == null ? NotFound() : Ok(dividas);
    }

    [HttpPost("Post")]
    public async Task<IActionResult> Post(
            [FromServices] AppDbContext context
            ,[FromBody] PostViewModel model
        )
    {
        if (!ModelState.IsValid)
            return BadRequest();

        if(context.Dividas.AsNoTracking().FirstOrDefault(x => x.CPF == model.CPF && x.pago == false) != null)
            return BadRequest("Não foi possível cadastrar a dívida pois o cliente já possui uma dívida não quitada.");

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
        catch (System.Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError); 
        }
    }

    [HttpPost("Put")]
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
            return NotFound("Uma dívida com este Id não foi encontrada");

        if (divida.pago)
            return UnprocessableEntity("Esta dívida já está paga e por isso não pode mais ser alterada.");
        
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
        catch (System.Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError); 
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
            return NotFound("Uma dívida com este Id não foi encontrada");

        if (divida.pago)
            return UnprocessableEntity("Esta dívida já está paga e por isso não pode mais ser alterada.");

        try
        {
            context.Dividas.Remove(divida);
            await context.SaveChangesAsync();
            return Ok($"Divida excluída. Id: {id}");
        }
        catch (System.Exception)
        {
            return BadRequest();
        }
    }
}

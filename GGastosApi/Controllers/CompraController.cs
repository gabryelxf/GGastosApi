using AutoMapper;
using GGastosApi.Data;
using GGastosApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace GGastosApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CompraController : ControllerBase
{

    //private static List<Compra> compras = new List<Compra>();
    private CompraContext _context;
    private IMapper _mapper;

    public CompraController(CompraContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Adiciona uma compra ao banco de dados
    /// </summary>
    /// <param name="compraDto">Objeto com os campos necessários para registro de uma compra</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult AdicionaCompra([FromBody] CreateCompraDto compraDto)
    {

        Compra compra = _mapper.Map<Compra>(compraDto);
        _context.compras.Add(compra);
        _context.SaveChanges();
        return CreatedAtAction(nameof(RecuperarCompraPorId), new { id = compra.IdCompra }, compra);
    }

    [HttpGet]
    public IEnumerable<ReadCompraDto> RecuperarCompras([FromQuery] int skip, [FromQuery] int take)
    {

        return _mapper.Map<List<ReadCompraDto>>(_context.compras.Skip(skip).Take(take));

    }

    [HttpGet("{id}")]
    public IActionResult RecuperarCompraPorId(int id)
    {

      
       var compra = _context.compras.FirstOrDefault(compra => compra.IdCompra == id);

        if (compra == null) return NotFound();
        var compraDto = _mapper.Map<ReadCompraDto>(compra);
        return Ok(compraDto);

    }

    [HttpGet, Route("api/RecuperarCompraPorLocal")]
    public IActionResult RecuperarCompraPorLocal(string local )
    {

          var compra = _context.compras.FirstOrDefault(compra => compra.Local.Contains(local));

        if (compra == null) return NotFound();
        var compraDto = _mapper.Map<ReadCompraDto>(compra);
        return Ok(compraDto);

    }

    [HttpGet, Route("api/RecuperarCompraPorAnoMes")]
    public IEnumerable<ReadCompraDto> RecuperarCompraPorAnoMes(int mes, int ano)
    {

        return _mapper.Map<List<ReadCompraDto>>(_context.compras.Where(compra => compra.DataInsercao.Month == mes && compra.DataInsercao.Year == ano));
        //var compra = _context.compras.FirstOrDefault(compra => compra.DataInsercao.Month == mes && compra.DataInsercao.Year == ano);

       // if (compra == null) return NotFound();
        //var compraDto = _mapper.Map<ReadCompraDto>(compra);
        //return Ok(compraDto);


    }



    [HttpPut("{id}")]
    public IActionResult AtualizaCompra(int id, [FromBody] UpdateCompraDto compraDto)
    {

        var compra = _context.compras.FirstOrDefault(compra => compra.IdCompra == id);

        if(compra == null) return NotFound();
        _mapper.Map(compraDto, compra);
        _context.SaveChanges();

        return NoContent();

    }

    [HttpPatch("{id}")]
    public IActionResult AtualizaCompraParcial(int id, JsonPatchDocument<UpdateCompraDto> patch)
    {

        var compra = _context.compras.FirstOrDefault(compra => compra.IdCompra == id);

        if (compra == null) return NotFound();
        var compraParaAtualizar = _mapper.Map<UpdateCompraDto>(compra);
        patch.ApplyTo(compraParaAtualizar, ModelState);
        if (!TryValidateModel(compraParaAtualizar)) return ValidationProblem(ModelState);
        _mapper.Map(compraParaAtualizar, compra);
        _context.SaveChanges();


        return NoContent();

    }

    [HttpDelete("{id}")]
    public IActionResult DeletaCompra(int id)
    {

        var compra = _context.compras.FirstOrDefault(compra => compra.IdCompra == id);

        _context.Remove(compra);
        _context.SaveChanges();
        return NoContent();

    }
}

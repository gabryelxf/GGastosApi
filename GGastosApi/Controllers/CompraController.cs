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



    /// <summary>
    /// Recupera compras do banco de dados
    /// </summary>
    /// <param name="skip">Quantidade de linhas que deverão ser ignoradas</param>
    /// <param name="take">Quantidade de linhas que deverão ser recuperadas</param>
    /// <returns>IEnumerable</returns>
    /// <response code="200">Caso a recuperação dos dados seja feita com sucesso</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IEnumerable<ReadCompraDto> RecuperarCompras([FromQuery] int skip, [FromQuery] int take)
    {

        return _mapper.Map<List<ReadCompraDto>>(_context.compras.Skip(skip).Take(take));

    }

    /// <summary>
    /// Recupera uma compra do banco de dados por Id
    /// </summary>
    /// <param name="id">Id da compra a ser recuperada</param>
    /// <returns>IActionResult</returns>
    /// <response code="200">Caso a recuperação dos dados seja feita com sucesso</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult RecuperarCompraPorId(int id)
    {

      
       var compra = _context.compras.FirstOrDefault(compra => compra.IdCompra == id);

        if (compra == null) return NotFound();
        var compraDto = _mapper.Map<ReadCompraDto>(compra);
        return Ok(compraDto);

    }

    /// <summary>
    ///  Recupera compras do banco de dados por Local
    /// </summary>
    /// <param name="local">Local da compra a ser recuperada</param>
    /// <returns>IActionResult</returns>
    /// <response code="200">Caso a recuperação dos dados seja feita com sucesso</response>
    [HttpGet, Route("api/RecuperarCompraPorLocal")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult RecuperarCompraPorLocal(string local )
    {

          var compra = _context.compras.FirstOrDefault(compra => compra.Local.Contains(local));

        if (compra == null) return NotFound();
        var compraDto = _mapper.Map<ReadCompraDto>(compra);
        return Ok(compraDto);

    }


    /// <summary>
    /// Recupera compras do banco de dados pela dataInsercao (Ano/Mês)
    /// </summary>
    /// <param name="mes">Mês da dataInsercao da compra a ser recuperada</param>
    /// /// <param name="ano">Ano da dataInsercao da compra a ser recuperada</param>
    /// <returns>IEnumerable</returns>
    /// <response code="200">Caso a recuperação dos dados seja feita com sucesso</response>
    [HttpGet, Route("api/RecuperarCompraPorAnoMes")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IEnumerable<ReadCompraDto> RecuperarCompraPorAnoMes(int mes, int ano)
    {

        return _mapper.Map<List<ReadCompraDto>>(_context.compras.Where(compra => compra.DataInsercao.Month == mes && compra.DataInsercao.Year == ano));
        //var compra = _context.compras.FirstOrDefault(compra => compra.DataInsercao.Month == mes && compra.DataInsercao.Year == ano);

       // if (compra == null) return NotFound();
        //var compraDto = _mapper.Map<ReadCompraDto>(compra);
        //return Ok(compraDto);


    }


    /// <summary>
    /// Edita uma Compra do banco de dados pelo Id
    /// </summary>
    /// <param name="id">Id da compra a ser editada</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso a edição dos dados seja feita com sucesso</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult AtualizaCompra(int id, [FromBody] UpdateCompraDto compraDto)
    {

        var compra = _context.compras.FirstOrDefault(compra => compra.IdCompra == id);

        if(compra == null) return NotFound();
        _mapper.Map(compraDto, compra);
        _context.SaveChanges();

        return NoContent();

    }

    /// <summary>
    /// Edita uma Compra do banco de dados pelo Id
    /// </summary>
    /// <param name="id">Id da compra a ser editada</param>
    /// <returns>IActionResult</returns>
    /// <response code="200">Caso a edição dos dados seja feita com sucesso</response>
    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
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



    /// <summary>
    /// Exclui uma Compra do banco de dados pelo Id
    /// </summary>
    /// <param name="id">Id da compra a ser excluída</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso a deleção dos dados seja realizada com sucesso</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult DeletaCompra(int id)
    {

        var compra = _context.compras.FirstOrDefault(compra => compra.IdCompra == id);

        _context.Remove(compra);
        _context.SaveChanges();
        return NoContent();

    }
}

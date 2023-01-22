using GGastosApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace GGastosApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CompraController : ControllerBase
{

    private static List<Compra> compras = new List<Compra>();

    [HttpPost]
    public IActionResult AdicionaCompra([FromBody] Compra compra)
    {

        compras.Add(compra);
        return CreatedAtAction(nameof(RecuperarCompraPorId), new { id = compra.IdCompra }, compra);
    }

    [HttpGet]
    public IEnumerable<Compra> RecuperarCompras([FromQuery] int skip, [FromQuery] int take)
    {

        return compras.Skip(skip).Take(take);

    }

    [HttpGet("{id}")]
    public IActionResult RecuperarCompraPorId(int id)
    {

        var compra =  compras.FirstOrDefault(compra => compra.IdCompra == id );

        if (compra == null) return NotFound();
        return Ok(compra);

    }

}

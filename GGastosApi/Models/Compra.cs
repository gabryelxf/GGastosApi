using System.ComponentModel.DataAnnotations;

namespace GGastosApi.Models;

public class Compra
{

    private static int id = 0;

    public int IdCompra { get; }

    public Compra()
    {
        id++;
        this.IdCompra = id;
    }

    [Required(ErrorMessage = "Campo Local é obrigatório")]
    [MaxLength(50, ErrorMessage = "Campo Local precisa ter no máximo 50 caracteres")]
    public string Local { get; set; }
    [Required(ErrorMessage = "Campo Descrição é obrigatório")]
    [MaxLength(100, ErrorMessage = "Campo descrição precisa ter no máximo 100 caracteres")]
    public string Descricao { get; set; }

    [Required(ErrorMessage = "Campo Valor é obrigatório")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Campo valor precisa ser maior que 0")]
    public double Valor { get; set; }

    private DateTime dataInsercao = DateTime.Now;

    public DateTime DataInsercao 
    {
        get { return this.dataInsercao; }
     
    }
}

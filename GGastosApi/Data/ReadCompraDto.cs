using System.ComponentModel.DataAnnotations;

namespace GGastosApi.Data;

public class ReadCompraDto
{

    [Key]
    [Required]
    public int IdCompra { get; set; }

    [Required(ErrorMessage = "Campo Local é obrigatório")]
    [StringLength(50, ErrorMessage = "Campo Local precisa ter no máximo 50 caracteres")]
    public string Local { get; set; }

    [Required(ErrorMessage = "Campo Descrição é obrigatório")]
    [StringLength(100, ErrorMessage = "Campo descrição precisa ter no máximo 100 caracteres")]
    public string Descricao { get; set; }

    [Required(ErrorMessage = "Campo Valor é obrigatório")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Campo valor precisa ser maior que 0")]
    public double Valor { get; set; }
    
    [Required]
    public DateTime DataInsercao { get; set; }


    public DateTime DataConsulta { get; private set; } = DateTime.Now;

}

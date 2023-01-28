using GGastosApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GGastosApi.Data;

public class CompraContext : DbContext
{

    public DbSet<Compra> compras { get; set; }
    public CompraContext(DbContextOptions<CompraContext> opts) : base(opts)
    {

    }

}

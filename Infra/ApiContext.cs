using Domain.Caronas;
using Domain.Usuarios;
using Domain.Veiculos;
using Infra.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Infra;

public class ApiContext : DbContext
{
    public ApiContext(DbContextOptions<ApiContext>  options)  : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder
            .ApplyConfiguration(new VeiculoMapping())
            .ApplyConfiguration(new UsuarioMapping())
            .ApplyConfiguration(new CaronaMapping());
    }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Carona> Caronas { get; set; }
    public DbSet<Veiculo> Veiculos { get; set; }
}
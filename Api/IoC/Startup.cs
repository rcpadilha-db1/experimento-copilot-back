using Domain.Caronas.interfaces;
using Domain.Caronas.Services;
using Domain.Caronas.Validators;
using Domain.Usuarios.Interfaces;
using Domain.Veiculos.Interfaces;
using Infra;
using Infra.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Api.IoC;

public static class Startup
{
    public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        => services
            .AddDbContext(configuration)
            .AddRepositories()
            .AddServices()
            .AddValidators();
    
    public static void RunMigration<T>(IApplicationBuilder app) where T : DbContext
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        var context = serviceScope.ServiceProvider.GetService<T>();
        context.Database.Migrate();
    }
    
    private static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
        => services.AddDbContext<ApiContext>(
            options => options.UseMySql(
                configuration["ConnectionStrings:CaronaDatabase"],
                new MySqlServerVersion(new Version(8, 0, 23)), 
                mySqlOptions => mySqlOptions.CommandTimeout(600)));

    private static IServiceCollection AddRepositories(this IServiceCollection services)
        => services
            .AddScoped<ICaronaRepository, CaronaRepository>()
            .AddScoped<IUsuarioRepository, UsuarioRepository>()
            .AddScoped<IVeiculoRepository, VeiculoRepository>();
    
    private static IServiceCollection AddServices(this IServiceCollection services)
        => services
            .AddScoped<IListagemCaronaService, ListagemCaronaService>()
            .AddScoped<IRemocaoCaronaService, RemocaoCaronaService>()
            .AddScoped<ICadastroCaronaService, CadastroCaronaService>();
    
    private static IServiceCollection AddValidators(this IServiceCollection services)
        => services.AddScoped<ICadastroCaronaValidator, CadastroCaronaValidator>();
}
using System.Reflection;
using AutoMapper;
using Experimento.Application.Data;
using Experimento.Application.Services;
using Experimento.Application.Services.Behaviors;
using Experimento.Application.Services.Interfaces;
using Experimento.Application.UseCases.CreateRide;
using Experimento.Application.UseCases.DeleteRideById;
using Experimento.Data.Persistence;
using Experimento.Data.Repositories;
using Experimento.Domain.Interfaces;
using Experimento.Domain.Notification;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Experimento.Data;

public static class DependencyInjectionExtensions
{
    public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ExperimentoContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("ExperimentoDB")));

        services.AddScoped<IDatabaseContext, ExperimentoContext>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IRideRepository, RideRepository>();
    }
    
    public static void AddMediator(this IServiceCollection services)
    {
        services.AddMediatR(typeof(ValidationBehavior<,>).GetTypeInfo().Assembly);

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    }
    
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<ICheckRideRequirementsService, CheckRideRequirementsRequirementsService>();
        services.AddScoped<ICheckIfRideExistsService, CheckIfRideExistsService>();
        services.AddScoped<ICheckIfRideExistsService, CheckIfRideExistsService>();
        services.AddScoped<IListRidesByRiderIdService, ListRidesByRiderIdService>();
        services.AddScoped<NotificationContext>();
        
        services.AddMvc(options => options.Filters.Add<NotificationFilter>())
            .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
    }
    public static void AddValidators(this IServiceCollection services) =>
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    
    public static void AddAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<CreateRiderMapper>();
            cfg.AddProfile<DeleteRideByIdMapper>();
        }, typeof(DependencyInjectionExtensions).Assembly);
    }
}


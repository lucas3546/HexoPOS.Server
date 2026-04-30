using System.Reflection;
using FluentValidation;
using HexaPOS.Application.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HexaPOS.Application;

public static class ConfigureServices
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
        builder.Services.Scan(scan => scan
            .FromApplicationDependencies()
            .AddClasses(classes => classes
                .AssignableTo(typeof(IHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
    }
}
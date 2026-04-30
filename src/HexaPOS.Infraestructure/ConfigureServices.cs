using HexaPOS.Application.Common.Interfaces;
using HexaPOS.Infraestructure.Persistence;
using HexaPOS.Infraestructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HexaPOS.Infraestructure;

public static class ConfigureServices
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        var databaseConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        if (databaseConnectionString is null)
            throw new ArgumentNullException(nameof(databaseConnectionString));

        builder.Services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        builder.Services.AddDbContext<ApplicationDbContext>(
            (sp, options) =>
            {
                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                options.UseNpgsql(databaseConnectionString);
            }
        );
        builder.Services.AddScoped<IApplicationDbContext>(provider =>
            provider.GetRequiredService<ApplicationDbContext>()
        );
    }
}
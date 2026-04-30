using Microsoft.AspNetCore.HttpLogging;

namespace HexaPOS.Web;

public static class ConfigureServices
{
    public static void AddWebServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddOpenApi();
        
        builder.Services.AddEndpointsApiExplorer();
        
        builder.Services.AddRouting(options =>
        {
            options.LowercaseUrls = true;
            options.LowercaseQueryStrings = true;
        });
        
        builder.Services.AddHealthChecks();

         
        builder.Services.AddHttpLogging(o =>
        {
            o.LoggingFields = HttpLoggingFields.RequestMethod
                              | HttpLoggingFields.RequestPath
                              | HttpLoggingFields.ResponseStatusCode
                              | HttpLoggingFields.Duration;
        });
        
        builder.Services.AddHttpContextAccessor();
    }
}
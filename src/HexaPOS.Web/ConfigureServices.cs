using HexaPOS.Application.Common.Interfaces;
using HexaPOS.Web.Infraestructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

        builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
             .AddJwtBearer(options =>
             {
                 string? jwtKey = builder.Configuration["JWT:Key"];
                 if (string.IsNullOrEmpty(jwtKey)) throw new ArgumentNullException("JWT KEY is null or empty");

                 options.SaveToken = true;
                 options.RequireHttpsMetadata = false;
                 options.TokenValidationParameters = new TokenValidationParameters()
                 {

                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidAudience = builder.Configuration["JWT:Audience"],
                     ValidIssuer = builder.Configuration["JWT:Issuer"],
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
                 };

             });

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("Moderation", policy =>
                policy.RequireRole("Admin", "Mod")
            );
        });
    }
}
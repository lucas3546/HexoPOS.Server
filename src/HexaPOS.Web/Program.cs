using HexaPOS.Application;
using HexaPOS.Infraestructure;
using HexaPOS.Web;
using HexaPOS.Web.Endpoints;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddInfraestructureServices();
builder.AddApplicationServices();
builder.AddWebServices();

var app = builder.Build();

HexaPOS.Infraestructure.ConfigureServices.ApplyMigrations(app);


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference("/docs", options =>
    {
        options.WithTitle("HexaPOS API Documentation");
    });
}

app.MapHealthChecks("/health");
app.UseHttpLogging();
app.UseStatusCodePages();
app.UseHttpsRedirection();
app.MapAuthEndpoints();
app.MapProductsEndpoints();
app.MapCategoriesEndpoints();

app.Run();

using HexaPOS.Application;
using HexaPOS.Infraestructure;
using HexaPOS.Infraestructure.Persistence;
using HexaPOS.Web;
using HexaPOS.Web.Endpoints;
using Microsoft.Extensions.FileProviders;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

Directory.CreateDirectory("wwwroot/media");

builder.AddInfraestructureServices();
builder.AddApplicationServices();
builder.AddWebServices();

var app = builder.Build();

HexaPOS.Infraestructure.ConfigureServices.ApplyMigrations(app);
await app.InitialiseDatabaseAsync();

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
app.MapSalesEndpoints();
app.MapSaleItemsEndpoints();
app.MapMediaEndpoints();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.WebRootPath, "media")),
    RequestPath = "/media"
});

app.Run();

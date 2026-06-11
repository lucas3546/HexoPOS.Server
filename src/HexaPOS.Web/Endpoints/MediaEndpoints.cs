using HexaPOS.Application.Common.Interfaces;
using HexaPOS.Application.Common.Models;
using HexaPOS.Application.Features.Categories;
using HexaPOS.Application.Features.Media;
using HexaPOS.Web.Infraestructure.Extensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HexaPOS.Web.Endpoints
{
    public static class MediaEndpoints
    {
        public static void MapMediaEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/media")
                .WithTags("Media");

            group.MapPost("/bulk-upload", BulkUpload).DisableAntiforgery();
        }

        public static async Task<IResult> BulkUpload([FromForm] BulkSaveMediaRequest request, IHandler<BulkSaveMediaRequest, Result> handler, CancellationToken ct)
        {
            var response = await handler.HandleAsync(request, ct);

            return response.ToHttpResult();
        }
    }
}

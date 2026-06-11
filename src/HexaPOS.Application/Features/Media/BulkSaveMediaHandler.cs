using HexaPOS.Application.Common.Interfaces;
using HexaPOS.Application.Common.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace HexaPOS.Application.Features.Media
{
    public class BulkSaveMediaRequest
    {
        public required IFormFileCollection Files { get; init; }
    };

    public class BulkSaveMediaHandler : IHandler<BulkSaveMediaRequest, Result>
    {
        private readonly IFileStorageService _fileStorageService;
        public BulkSaveMediaHandler(IFileStorageService fileStorageService)
        {
            _fileStorageService = fileStorageService;
        }
        public async Task<Result> HandleAsync(BulkSaveMediaRequest request, CancellationToken ct)
        {
            foreach (IFormFile file in request.Files)
            {
                await using Stream stream = file.OpenReadStream();

                await _fileStorageService.SaveAsync(
                    stream,
                    file.FileName,
                    ct);
            }

            return Result.Ok();
        }
    }
}

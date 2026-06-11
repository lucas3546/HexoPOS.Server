using HexaPOS.Application.Common.Interfaces;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace HexaPOS.Infraestructure.Services;

public class FileStorageService : IFileStorageService
{
    private readonly string _mediaPath;

    public FileStorageService(IWebHostEnvironment environment)
    {
        _mediaPath = Path.Combine(environment.WebRootPath, "media");

        Directory.CreateDirectory(_mediaPath);
    }

    public async Task<string> SaveAsync(
        Stream stream,
        string fileName,
        CancellationToken cancellationToken = default)
    {
        string path = Path.Combine(_mediaPath, fileName);

        await using FileStream fileStream = File.Create(path);

        await stream.CopyToAsync(fileStream, cancellationToken);

        return fileName;
    }

    public async Task<bool> DeleteAsync(
        string fileName,
        CancellationToken cancellationToken = default)
    {
        string path = Path.Combine(_mediaPath, fileName);

        if (!File.Exists(path))
            return false;

        await Task.Run(() => File.Delete(path), cancellationToken);

        return true;
    }

    public bool Exists(string fileName)
    {
        string path = Path.Combine(_mediaPath, fileName);

        return File.Exists(path);
    }

}
using System;
using System.Collections.Generic;
using System.Text;

namespace HexaPOS.Application.Common.Interfaces
{
    public interface IFileStorageService
    {
        Task<string> SaveAsync(Stream stream, string fileName, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(string fileName, CancellationToken cancellationToken = default);
        bool Exists(string fileName);
    }
}

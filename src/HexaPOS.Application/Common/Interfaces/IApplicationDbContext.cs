using HexaPOS.Domain.Entites;
using Microsoft.EntityFrameworkCore;

namespace HexaPOS.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Category> Categories { get; }
    DbSet<Product> Products { get; }
    DbSet<Sale> Sales { get; }
    DbSet<SaleItem> SaleItems { get; }
    DbSet<Account> Users { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
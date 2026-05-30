using HexaPOS.Application.Common.Interfaces;
using HexaPOS.Domain.Entites;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HexaPOS.Infraestructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<Account>, IApplicationDbContext
{
    public ApplicationDbContext() { } 
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);                
        
    }

    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Sale> Sales =>  Set<Sale>();
    public DbSet<SaleItem> SaleItems => Set<SaleItem>();
    public DbSet<Account> Users => Set<Account>();
}
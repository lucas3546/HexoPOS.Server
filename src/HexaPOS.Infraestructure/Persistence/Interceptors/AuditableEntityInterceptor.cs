using HexaPOS.Application.Common.Interfaces;
using HexaPOS.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace HexaPOS.Infraestructure.Persistence.Interceptors;

public class AuditableEntityInterceptor : SaveChangesInterceptor
{
    private readonly ICurrentUserService _user;
    private readonly TimeProvider _dateTime;

    public AuditableEntityInterceptor(
        ICurrentUserService user,
        TimeProvider dateTime)
    {
        _user = user;
        _dateTime = dateTime;
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);

        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        UpdateEntities(eventData.Context);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public void UpdateEntities(DbContext? context)
    {
        if (context == null) return;


        var entries = context.ChangeTracker.Entries()
            .Where(e => e.Entity is IAuditableEntity &&
                        (e.State == EntityState.Added 
                        || e.State == EntityState.Modified 
                        || e.HasChangedOwnedEntities()));

        foreach (var entry in entries)
        {
            var entity = (IAuditableEntity)entry.Entity;
            var utcNow = _dateTime.GetUtcNow();

            if (entry.State == EntityState.Added)
            {
                if (!string.IsNullOrEmpty(_user.Id))
                    entity.CreatedBy = _user.Id;

                entity.CreatedAt = utcNow;
            }

            if (!string.IsNullOrEmpty(_user.Id))
                entity.UpdatedBy = _user.Id;

            entity.UpdatedAt = utcNow;
        }   
    }
}


public static class Extensions
{
    public static bool HasChangedOwnedEntities(this EntityEntry entry) =>
        entry.References.Any(r => 
            r.TargetEntry != null && 
            r.TargetEntry.Metadata.IsOwned() && 
            (r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified));
}
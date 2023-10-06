
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using MovieTicketer.Persistence.Entities;
using MovieTicketer.Persistence.Wrappers;

namespace MovieTicketer.Persistence.Interceptors;


public class AuditSaveChangesInterceptor : SaveChangesInterceptor
{

  private readonly IDateTimeOffsetWrapper _dateTimeOffsetWrapper;

  public AuditSaveChangesInterceptor(
      IDateTimeOffsetWrapper dateTimeOffsetWrapper)
  {
    _dateTimeOffsetWrapper = dateTimeOffsetWrapper;
  }

  public override InterceptionResult<int> SavingChanges(DbContextEventData eventData,
      InterceptionResult<int> result)
  {
    UpdateEntities(eventData.Context);

    return base.SavingChanges(eventData, result);
  }

  public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
      InterceptionResult<int> result,
      CancellationToken cancellationToken = default)
  {
    UpdateEntities(eventData.Context);

    return base.SavingChangesAsync(eventData, result, cancellationToken);
  }

  private void UpdateEntities(DbContext? context)
  {
    if (context is null)
      return;

    foreach (var entry in context.ChangeTracker.Entries<Entity>())
    {
      if (entry.State == EntityState.Added)
      {
        entry.Property<DateTimeOffset>("CreatedAt").CurrentValue = _dateTimeOffsetWrapper.UtcNow;
      }

      if (entry.State is not EntityState.Added and not EntityState.Modified)
        continue;

      entry.Property<DateTimeOffset>("LastModifiedAt").CurrentValue = _dateTimeOffsetWrapper.UtcNow;
    }
  }
}

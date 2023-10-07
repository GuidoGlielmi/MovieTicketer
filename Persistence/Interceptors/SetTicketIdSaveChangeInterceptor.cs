using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using MovieTicketer.Persistence.Entities;

namespace MovieTicketer.Persistence.Interceptors;

public class SetTicketIdSaveChangeInterceptor : SaveChangesInterceptor
{
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

  private static void UpdateEntities(DbContext? context)
  {
    if (context is null)
      return;
    //foreach (var entry in context.ChangeTracker.Entries<Ticket>())
    //{
    //  if (entry.State == EntityState.Added)
    //  {
    //    entry.Property<string>("Id").CurrentValue = $"{entry.Entity.RowIdentifier}{entry.Entity.ColumnIdentifier}{entry.Entity.Show.RoomId}";
    //  }
    //}
  }
}

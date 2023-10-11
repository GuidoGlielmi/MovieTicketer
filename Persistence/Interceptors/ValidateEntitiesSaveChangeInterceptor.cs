using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.ComponentModel.DataAnnotations;

namespace MovieTicketer.Persistence.Interceptors;

public class ValidateEntitiesSaveChangeInterceptor : SaveChangesInterceptor
{
  public override InterceptionResult<int> SavingChanges(DbContextEventData eventData,
      InterceptionResult<int> result)
  {
    ValidateEntities(eventData.Context);

    return base.SavingChanges(eventData, result);
  }

  public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
      InterceptionResult<int> result,
      CancellationToken cancellationToken = default)
  {
    ValidateEntities(eventData.Context);

    return base.SavingChangesAsync(eventData, result, cancellationToken);
  }

  private static void ValidateEntities(DbContext? context)
  {
    if (context is null)
      return;

    context.ChangeTracker
    .Entries()
    .Where(e => e.State is EntityState.Added or EntityState.Modified)
    .Select(e => e.Entity)
    .ToList()
    .ForEach(entity =>
    {
      var validationContext = new ValidationContext(entity);
      Validator.ValidateObject(entity, validationContext, validateAllProperties: true);
    });
  }
}

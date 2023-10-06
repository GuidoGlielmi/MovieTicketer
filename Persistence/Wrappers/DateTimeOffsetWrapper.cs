using MovieTicketer.Persistence.Wrappers;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class DateTimeOffsetWrapper : IDateTimeOffsetWrapper
{
  public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
}

namespace MovieTicketer.Persistence.Wrappers;

public interface IDateTimeOffsetWrapper
{
  DateTimeOffset UtcNow { get; }
}

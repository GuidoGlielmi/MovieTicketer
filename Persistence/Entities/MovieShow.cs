using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTicketer.Persistence.Entities;

public class MovieShow : Entity
{
  public int MovieId { get; init; }

  public virtual required Movie Movie { get; init; }

  public required DateTime StartTime { get; init; }

  [NotMapped]
  public DateTime EndTime => StartTime + Movie.Duration;

  public List<Ticket> Tickets { get; } = new();

  public int RoomId { get; init; }

  public virtual required Room Room { get; init; }
}

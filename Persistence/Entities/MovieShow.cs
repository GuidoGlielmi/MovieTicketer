using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTicketer.Persistence.Entities;

public class MovieShow : Entity
{
  public int MovieId { get; init; }

  public virtual required Movie Movie { get; init; }

  public List<Ticket> Tickets { get; } = new();

  public int RoomId { get; init; }

  public virtual required Room Room { get; init; }

  public required DateTime StartTime { get; init; }

  [NotMapped]
  public DateTime EndTime => StartTime + Movie.Duration;

  [NotMapped]
  public Dictionary<(char, int), bool> AvailableSeats
  {
    get
    {
      var seatsState = new Dictionary<(char, int), bool>();
      for (char row = 'a'; row < Room.RowsAmount; row++)
      {
        for (int column = 0; column < Room.ColumnsAmount; column++)
        {
          seatsState.Add((row, column), !Tickets.Exists(t => t.RowIdentifier == row && t.ColumnIdentifier == column));
        }
      }
      return seatsState;
    }
  }
}


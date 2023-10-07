using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTicketer.Persistence.Entities;

public class Show : Entity
{
  private float _price;
  public required float Price
  {
    get => (float)(_price * (IsPremiering ? 1.25 : 1) + _price * ((int)Movie.Format) / 100);
    set => _price = value;
  }

  public required bool IsPremiering { get; init; }

  public Guid MovieId { get; init; }

  public virtual required Movie Movie { get; init; }

  private readonly List<Ticket> _tickets = new();
  public IReadOnlyList<Ticket> Tickets => _tickets.AsReadOnly();

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
          seatsState.Add((row, column), !_tickets.Any(t => t.RowIdentifier == row && t.ColumnIdentifier == column));
        }
      }
      return seatsState;
    }
  }

  public void AddTicket(Ticket ticket)
  {
    if (_tickets.Any(t => t.RowIdentifier == ticket.RowIdentifier && t.ColumnIdentifier == ticket.ColumnIdentifier))
      throw new Exception();
    _tickets.Add(ticket);
  }

  public void RemoveTicket(Ticket ticket)
  {
    _tickets.Remove(ticket);
  }
}


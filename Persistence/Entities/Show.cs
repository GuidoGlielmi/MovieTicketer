using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTicketer.Persistence.Entities;

public class Show : Entity
{
  private readonly List<Ticket> _tickets = new();

  private float _price;
  public float BasePrice => _price;
  public required float Price
  {
    get => (float)(_price * ((IsPremiering ? 1.25 : 1) + Movie.FormatSurcharge));
    set => _price = value;
  }

  public required bool IsPremiering { get; init; }

  public required Movie Movie { get; init; }

  public IReadOnlyList<Ticket> Tickets => _tickets.AsReadOnly();

  public required Room Room { get; init; }

  public required DateTime StartTime { get; init; }

  [NotMapped]
  public DateTime EndTime => StartTime + Movie.Duration; // estimated

  [NotMapped]
  public Dictionary<(char, int), bool> AvailableSeats
  {
    get
    {
      var seatsState = new Dictionary<(char, int), bool>();
      for (char row = 'a'; row < Room.RowsCount; row++)
      {
        for (int column = 0; column < Room.ColumnsCount; column++)
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



using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTicketer.Persistence.Entities;

public class Ticket : Entity
{
  public Ticket(Guid movieShowId, MovieShow movieShow, float price, Guid buyerId, Buyer buyer, char rowIdentifier, int columnIdentifier)
  : this(movieShow, price, buyer, rowIdentifier, columnIdentifier)
  {
    MovieShowId = movieShowId;
    BuyerId = buyerId;
  }

  public Ticket(MovieShow movieShow, float price, Buyer buyer, char rowIdentifier, int columnIdentifier)
  {
    Id = $"{RowIdentifier}{ColumnIdentifier}{movieShow.RoomId}";
    MovieShow = movieShow;
    Price = price;
    Buyer = buyer;
    RowIdentifier = rowIdentifier;
    ColumnIdentifier = columnIdentifier;
  }

  public new string Id { get; init; }

  public Guid MovieShowId { get; init; }

  public virtual MovieShow MovieShow { get; init; }

  public float Price { get; set; }

  public Guid BuyerId { get; init; }

  public virtual Buyer Buyer { get; init; }

  public char RowIdentifier { get; init; }

  public int ColumnIdentifier { get; init; }

  [NotMapped]
  public string SeatId => $"{RowIdentifier}{ColumnIdentifier}";
}
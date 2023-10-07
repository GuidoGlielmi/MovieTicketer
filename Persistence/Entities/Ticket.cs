
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTicketer.Persistence.Entities;

public class Ticket : Entity
{
  public Guid ShowId { get; init; }

  public required virtual Show Show { get; init; }

  public Guid BuyerId { get; init; }

  public required virtual Buyer Buyer { get; init; }

  public required char RowIdentifier { get; init; }

  public required int ColumnIdentifier { get; init; }

  [NotMapped]
  public string SeatId => $"{RowIdentifier}{ColumnIdentifier}";
}
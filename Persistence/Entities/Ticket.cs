
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTicketer.Persistence.Entities;

public class Ticket : Entity
{
  private float _price;
  public float BasePrice => _price;
  public required float Price
  {
    get => (float)(_price * (1 + (Show.IsPremiering ? 0.25 : 0) + Show.Movie.FormatSurcharge + (IsPremium ? 0.2 : 0)));
    set => _price = value;
  }

  public required bool IsPremium { get; set; }

  public required Show Show { get; init; }

  public required Buyer Buyer { get; init; }

  public required char RowIdentifier { get; init; }

  public required int ColumnIdentifier { get; init; }

  [NotMapped]
  public string SeatId => $"{RowIdentifier}{ColumnIdentifier}";
}
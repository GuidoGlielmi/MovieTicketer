namespace MovieTicketer.Persistence.Entities;

public class Ticket : Entity
{
  public Guid MovieShowId { get; init; }

  public virtual required MovieShow MovieShow { get; init; }

  public required float Price { get; set; }

  public Guid BuyerId { get; init; }

  public virtual required Buyer Buyer { get; init; }
}

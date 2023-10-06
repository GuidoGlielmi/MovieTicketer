namespace MovieTicketer.Persistence.Entities;

public class Buyer : Entity
{
  public required string FirstName { get; init; }

  public required string LastName { get; init; }

  public required string DNI { get; init; }

  public required int Age { get; init; }
}

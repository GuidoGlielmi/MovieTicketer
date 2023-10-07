
using MovieTicketer.Persistence.Entities;

namespace MovieTicketer.Persistence.Seeds;
public static class Seed
{
  public static void SeedDatabase(this IServiceCollection services)
  {
    using var context = services.BuildServiceProvider().GetService<MovieTicketerDbContext>()!;
    using var transaction = context.Database.BeginTransaction();
    try
    {
      var starOne = new Starrer()
      {
        Age = 20,
        Name = "John",
      };
      var starTwo = new Starrer()
      {
        Age = 20,
        Name = "John",
      };

      var movie = new Movie
      {
        Categories = new() { MovieCategory.Action, MovieCategory.Comedy },
        Duration = new TimeSpan(2, 30, 0),
        Rate = MovieRate.G,
        Title = "Tuvieja",
        Starrers = new() { starOne, starTwo },
        Format = MovieFormat.Standard,
      };

      var room = new Room
      {
        RowsAmount = 20,
        ColumnsAmount = 20,
      };

      var show = new Show
      {
        Movie = movie,
        Room = room,
        Price = 3,
        StartTime = DateTime.UtcNow,
        IsPremiering = true,
      };

      var buyer = new Buyer
      {
        Age = 20,
        DNI = "37700560",
        FirstName = "asd",
        LastName = "asd",
      };

      show.AddTicket(new Ticket()
      {
        Show = show,
        Buyer = buyer,
        RowIdentifier = 'a',
        ColumnIdentifier = 3
      });

      context.Show.Add(show);

      context.SaveChanges();
      transaction.Commit();
    } catch (Exception e)
    {
      Console.WriteLine(e);
    }
  }
}

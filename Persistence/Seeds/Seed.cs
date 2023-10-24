
using MovieTicketer.Persistence.Entities;

namespace MovieTicketer.Persistence.Seeds;
public static class Seed
{
  public static void SeedDatabase(IServiceCollection services)
  {
    using var context = services.BuildServiceProvider().GetService<MovieTicketerDbContext>()!;
    using var transaction = context.Database.BeginTransaction();
    try
    {
      var starOne = new Actor()
      {
        Age = 20,
        Name = "John",
        ActingRole = ActingRole.Co_Star
      };
      var starTwo = new Actor()
      {
        Age = 20,
        Name = "John",
        ActingRole = ActingRole.Guest_Star
      };

      var movie = new Movie
      {
        Duration = new TimeSpan(2, 30, 0),
        Rate = MovieRate.G,
        Title = "Tuvieja",
        Starrers = new() { starOne, starTwo },
        Format = MovieFormat.Standard,
      };
      movie.AddCategory(MovieCategory.Action, MovieCategory.Comedy);

      var room = new Room
      {
        RoomNumber = 0,
        RowsCount = 20,
        ColumnsCount = 20,
      };

      var show = new Show
      {
        Movie = movie,
        Room = room,
        StartTime = DateTime.UtcNow,
        IsPremiering = true,
      };

      var buyer = new Buyer
      {
        Age = 20,
        Dni = "37700560",
        FirstName = "asd",
        LastName = "asd",
      };

      show.AddTicket(new Ticket()
      {
        Show = show,
        Buyer = buyer,
        IsPremium = true,
        Price = 3,
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

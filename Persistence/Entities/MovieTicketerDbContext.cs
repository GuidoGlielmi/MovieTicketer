
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace MovieTicketer.Persistence.Entities;

public class MovieTicketerDbContext : DbContext, IMovieTicketerDbContext
{
  public MovieTicketerDbContext(DbContextOptions<MovieTicketerDbContext> options)
    : base(options)
  {
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

    base.OnModelCreating(modelBuilder);
  }
  public required DbSet<Movie> Categories { get; set; }
  public required DbSet<Room> Rooms { get; set; }
  public required DbSet<MovieShow> MovieShows { get; set; }
  public required DbSet<Movie> Movies { get; set; }
  public required DbSet<Ticket> Tickets { get; set; }
  public required DbSet<Starrer> Starrers { get; set; }
  public required DbSet<Buyer> Buyers { get; set; }
}


using Microsoft.EntityFrameworkCore;

namespace MovieTicketer.Persistence.Entities;

public class MovieTicketerDbContext : DbContext, IMovieTicketerDbContext
{
  public MovieTicketerDbContext(DbContextOptions<MovieTicketerDbContext> options)
    : base(options)
  {
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

    base.OnModelCreating(modelBuilder);
  }

  public DbSet<Room> Room { get; set; }
  public DbSet<Show> Show { get; set; }
  public DbSet<Movie> Movie { get; set; }
  public DbSet<Ticket> Ticket { get; set; }
  public DbSet<Actor> Actor { get; set; }
  public DbSet<Buyer> Buyer { get; set; }
}

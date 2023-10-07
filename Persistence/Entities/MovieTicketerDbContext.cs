
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
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
    //foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
    //{
    //  entityType.SetTableName(entityType.GetTableName()!.ToLower());
    //}

    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

    base.OnModelCreating(modelBuilder);
  }

  public DbSet<Room> Room { get; set; }
  public DbSet<Show> Show { get; set; }
  public DbSet<Movie> Movie { get; set; }
  public DbSet<Ticket> Ticket { get; set; }
  public DbSet<Starrer> Starrer { get; set; }
  public DbSet<Buyer> Buyer { get; set; }
}

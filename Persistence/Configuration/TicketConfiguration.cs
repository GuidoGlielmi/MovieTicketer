using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieTicketer.Persistence.Entities;

namespace MovieTicketer.Persistence.Configuration;

public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
{
  public void Configure(EntityTypeBuilder<Ticket> builder)
  {
    builder.Property(e => e.Id).ValueGeneratedNever();
  }
}
using FluentValidation;
using MovieTicketer.Persistence.Entities;

namespace MovieTicketer.Persistence.Validations;

public class TicketValidator : AbstractValidator<Ticket>
{
  const int LetterACharCode = 'a';
  public TicketValidator()
  {
    RuleFor(t => t.RowIdentifier).LessThanOrEqualTo(t => (char)(LetterACharCode + t.MovieShow.Room.RowsAmount));
    RuleFor(t => t.ColumnIdentifier).LessThanOrEqualTo(t => t.MovieShow.Room.ColumnsAmount);
  }
}
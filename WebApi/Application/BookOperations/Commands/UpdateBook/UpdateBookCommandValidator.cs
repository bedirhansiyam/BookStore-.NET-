using FluentValidation;

namespace WebApi.Application.BookOperations.Commands.UpdateBook;

public class UpdateBookCommandValidator: AbstractValidator<UpdateBookCommand>
{
    public UpdateBookCommandValidator()
    {
        RuleFor(command => command.Model.GenreId).GreaterThan(0).When(x => x.Model.GenreId != default);;
        RuleFor(command => command.Model.AuthorId).GreaterThan(0).When(x => x.Model.AuthorId != default);
        RuleFor(command => command.Model.PageCount).GreaterThan(0).When(x => x.Model.PageCount != default);
        RuleFor(command => command.Model.PublishDate.Date).LessThan(DateTime.Now).When(x => x.Model.PublishDate != DateTime.Now);
        RuleFor(command => command.Model.Title).MinimumLength(4).When(x => x.Model.Title != string.Empty);
        RuleFor(command => command.BookId).GreaterThan(0);

    }
}
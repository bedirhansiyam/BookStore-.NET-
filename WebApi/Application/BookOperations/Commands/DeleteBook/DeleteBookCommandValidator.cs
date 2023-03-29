using FluentValidation;
using WebApi.Application.BookOperations.Commands.DeleteBook;

namespace WebApi.Application.BookOperations.Commands.DeleteBook;


public class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>
{
    public DeleteBookCommandValidator()
    {
        RuleFor(command => command.BookId).GreaterThan(0);
    }
}
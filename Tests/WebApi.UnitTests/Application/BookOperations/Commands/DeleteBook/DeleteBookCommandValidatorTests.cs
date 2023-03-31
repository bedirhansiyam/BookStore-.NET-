using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.DeleteBook;

namespace Application.BookOperations.Commands.DeleteBook;

public class DeleteBookCommandValidatorTests: IClassFixture<CommonTestFixture>
{
    [Fact]
    public void WhenInvalidBookIdIsGiven_Validator_ShouldBeReturnError()
    {
        DeleteBookCommand command = new DeleteBookCommand(null);
        command.BookId = -1;

        DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidBookIdIsGiven_Validator_ShouldNotReturnError()
    {
        DeleteBookCommand command = new DeleteBookCommand(null);
        command.BookId = 2;

        DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().Be(0);
    }
}
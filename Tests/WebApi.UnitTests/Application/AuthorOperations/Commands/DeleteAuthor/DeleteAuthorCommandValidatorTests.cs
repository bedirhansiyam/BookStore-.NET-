using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands.DeleteAuthor;

namespace Application.AuthorOperations.Commands.DeleteAuthor;

public class DeleteAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
{
    [Fact]
    public void WhenInvalidAuthorIdIsGiven_DeleteAuthorValidator_ShouldReturnErrors()
    {
        DeleteAuthorCommand command = new DeleteAuthorCommand(null);
        command.AuthorId = -1;

        DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidAuthorIdIsGiven_DeleteAuthorValidator_ShouldNotReturnErrors()
    {
        DeleteAuthorCommand command = new DeleteAuthorCommand(null);
        command.AuthorId = 1;

        DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().Be(0);
    }
}
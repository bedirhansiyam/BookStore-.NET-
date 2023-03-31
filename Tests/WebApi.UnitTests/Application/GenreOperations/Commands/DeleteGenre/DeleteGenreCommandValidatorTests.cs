using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;

namespace Application.GenreOperations.Commands.DeleteGenre;

public class DeleteGenreCommandValidatorTests: IClassFixture<CommonTestFixture>
{
    [Fact]
    public void WhenInvalidGenreIdIsGiven_Validator_ShouldBeReturnError()
    {
        DeleteGenreCommand command = new DeleteGenreCommand(null);
        command.GenreId = -1;

        DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidGenreIdIsGiven_Validator_ShouldNotReturnError()
    {
        DeleteGenreCommand command = new DeleteGenreCommand(null);
        command.GenreId = 1;

        DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().Be(0);
    }
}
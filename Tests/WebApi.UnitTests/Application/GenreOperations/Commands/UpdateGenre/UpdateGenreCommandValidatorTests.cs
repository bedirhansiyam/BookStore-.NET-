using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;

namespace Application.GenreOperations.Commands.UpdateGenre;

public class UpdateGenreCommandValidatorTests: IClassFixture<CommonTestFixture>
{
    [Theory]
    [InlineData(" ")]
    [InlineData("T")]
    [InlineData("Te")]
    [InlineData("Tes")]
    public void WhenInvalidGenreNameIsGiven_UpdateGenreValidator_ShouldBeReturnErrors(string name)
    {
        UpdateGenreCommand command = new UpdateGenreCommand(null);
        command.Model = new UpdateGenreModel(){Name = name};

        UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Theory]
    [InlineData("")]
    [InlineData("Test")]
    [InlineData("Test Genre")]
    public void WhenValidGenreNameIsGiven_UpdateGenreValidator_ShouldNotReturnErrors(string name)
    {
        UpdateGenreCommand command = new UpdateGenreCommand(null);
        command.Model = new UpdateGenreModel(){Name = name};

        UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().Be(0);
    }
}
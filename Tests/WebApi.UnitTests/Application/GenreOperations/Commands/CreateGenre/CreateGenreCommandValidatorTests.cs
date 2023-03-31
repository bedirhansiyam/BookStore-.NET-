using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands.CreateGenre;

namespace Application.GenreOperations.Commands.CreateGenre;

public class CreateBookCommandValidatorTests : IClassFixture<CommonTestFixture>
{
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("T")]
    [InlineData("Te")]
    [InlineData("Tes")]
    public void WhenInvalidGenreNameIsGiven_CreateGenreValidator_ShouldBeReturnErrors(string name)
    {
        CreateGenreCommand command = new CreateGenreCommand(null);
        command.Model = new CreateGenreModel(){Name = name};

        CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0);
        
    }

    [Fact]
    public void WhenValidGenreNameIsGiven_CreateGenreValidator_ShouldNotReturnErrors()
    {
        CreateGenreCommand command = new CreateGenreCommand(null);
        command.Model = new CreateGenreModel(){Name = "Test Genre"};

        CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().Be(0);
        
    }
}
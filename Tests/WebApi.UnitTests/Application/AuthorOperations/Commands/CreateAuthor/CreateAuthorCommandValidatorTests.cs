using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using static WebApi.Application.AuthorOperations.Commands.CreateAuthor.CreateAuthorCommand;

namespace Application.AuthorOperations.Commands.CreateAuthor;

public class CreateAuthorCommandValidatorTests: IClassFixture<CommonTestFixture>
{
    [Theory]
    [InlineData("","")]
    [InlineData("N","")]
    [InlineData("","S")]
    [InlineData("N","S")]
    [InlineData("Na","S")]
    [InlineData("N","Su")]
    public void WhenInvalidInputsAreGiven_CreateAuthorValidator_ShouldBeReturnErrors(string name, string surname)
    {
        CreateAuthorCommand command = new CreateAuthorCommand(null,null);
        command.Model = new CreateAuthorModel(){Name = name, Surname = surname};

        CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenDateTimeEqualNowIsGiven_CreateBookValidator_ShouldBeReturnError()
    {
        CreateAuthorCommand command = new CreateAuthorCommand(null,null);
        command.Model = new CreateAuthorModel(){Name = "name", Surname = "surname", BirthDate = DateTime.Now.Date};

        CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenDateTimeEqualEmptyIsGiven_CreateBookValidator_ShouldBeReturnError()
    {
        CreateAuthorCommand command = new CreateAuthorCommand(null,null);
        command.Model = new CreateAuthorModel(){Name = "name", Surname = "surname"};

        CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputsAreGiven_CreateAuthorValidator_ShouldNotReturnErrors()
    {
        CreateAuthorCommand command = new CreateAuthorCommand(null,null);
        command.Model = new CreateAuthorModel(){Name = "name", Surname = "surname", BirthDate = DateTime.Now.Date.AddYears(-15)};

        CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().Be(0);
    }
}
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using static WebApi.Application.AuthorOperations.Commands.UpdateAuthor.UpdateAuthorCommand;

namespace Application.AuthorOperations.Commands.UpdateAuthor;

public class UpdateAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
{
    [Theory]
    [InlineData("N","",1)]
    [InlineData("","S",1)]
    [InlineData("N","S",0)]
    [InlineData("N","",0)]
    [InlineData("","S",0)]
    [InlineData("N","S",1)]
    public void WhenInvalidInputsAreGiven_UpdateAuthorValidator_ShouldBeReturnErrors(string name, string surname, int authorId)
    {
        UpdateAuthorCommand command = new UpdateAuthorCommand(null);
        command.AuthorId = authorId;
        command.Model = new UpdateAuthorModel(){Name = name, Surname = surname, BirthDate = DateTime.Now.Date.AddYears(-35)};

        UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenDateTimeEqualNowIsGiven_UpdateAuthorValidator_ShouldBeReturnErrors()
    {
        UpdateAuthorCommand command = new UpdateAuthorCommand(null);
        command.AuthorId = 1;
        command.Model = new UpdateAuthorModel(){Name = "name", Surname = "surname", BirthDate = DateTime.Now.Date};

        UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputsAreGiven_UpdateAuthorValidator_ShouldNotBeReturnErrors()
    {
        UpdateAuthorCommand command = new UpdateAuthorCommand(null);
        command.AuthorId = 1;
        command.Model = new UpdateAuthorModel(){Name = "name", Surname = "surname", BirthDate = DateTime.Now.Date.AddYears(-35)};

        UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().Be(0);
    }
}
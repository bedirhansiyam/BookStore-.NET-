using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.CreateBook;
using static WebApi.Application.BookOperations.Commands.CreateBook.CreateBookCommand;

namespace Application.BookOperations.Commands.CreateBook;

public class CreateBookCommandValidatorTests : IClassFixture<CommonTestFixture>
{
    [Theory]
    [InlineData("Lotr",0,0,0)]
    [InlineData("Lotr",0,1,1)]
    [InlineData("Lotr",1,0,1)]
    [InlineData("Lotr",1,1,0)]
    [InlineData("",0,0,0)]
    [InlineData("",0,1,1)]
    [InlineData("",1,1,0)]
    [InlineData("",1,0,1)]
    [InlineData("Lor",1,1,1)]
    [InlineData(" ",1,1,1)]
    public void WhenInvalidInputsAreGiven_CreateBookValidator_ShouldBeReturnErrors(string title, int PageCount, int GenreId, int AuthorId)
    {
        //arrange
        CreateBookCommand command = new CreateBookCommand(null, null);
        command.Model = new CreateBookModel(){
            Title=title,
            PageCount = PageCount,
            PublishDate = DateTime.Now.Date.AddYears(-1),
            GenreId = GenreId,
            AuthorId = AuthorId
        };

        //act
        CreateBookCommandValidator validator = new CreateBookCommandValidator();
        var result = validator.Validate(command);

        //assert
        result.Errors.Count.Should().BeGreaterThan(0);
    }
    [Fact]
    public void WhenDateTimeEqualNowIsGiven_CreateBookValidator_ShouldBeReturnError()
    {
        CreateBookCommand command = new CreateBookCommand(null, null);
        command.Model = new CreateBookModel(){
            Title="Lotr",
            PageCount = 100,
            PublishDate = DateTime.Now.Date,
            GenreId = 1,
            AuthorId = 1
        };

        CreateBookCommandValidator validator = new CreateBookCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0);
    }
    [Fact]
    public void WhenValidInputsAreGiven_CreateBookValidator_ShouldNotReturnError()
    {
        CreateBookCommand command = new CreateBookCommand(null, null);
        command.Model = new CreateBookModel(){
            Title="Lotr",
            PageCount = 100,
            PublishDate = DateTime.Now.Date.AddYears(-2),
            GenreId = 1,
            AuthorId = 1
        };

        CreateBookCommandValidator validator = new CreateBookCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().Be(0);
    }
}
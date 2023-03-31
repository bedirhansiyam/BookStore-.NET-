using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using static WebApi.Application.BookOperations.Commands.UpdateBook.UpdateBookCommand;

namespace Application.BookOperations.Commands.UpdateBook;

public class UpdateBookCommandValidatorTests : IClassFixture<CommonTestFixture>
{
    [Theory]
    [InlineData(0,"Lotr",0,0,0)]
    [InlineData(1,"Lotr",0,-1,1)]
    [InlineData(1,"Lor",1,0,1)]
    [InlineData(1,"Lotr",-1,1,0)]
    [InlineData(1,"",1,0,-1)]
    public void WhenInvalidInputsAreGiven_UpdateBookValidator_ShouldBeReturnErrors(int bookId, string title, int PageCount, int GenreId, int AuthorId)
    {
        UpdateBookCommand command = new UpdateBookCommand(null);
        command.BookId = bookId;
        command.Model = new UpdateBookModel(){
            Title=title,
            PageCount = PageCount,
            PublishDate = DateTime.Now.Date.AddYears(-1),
            GenreId = GenreId,
            AuthorId = AuthorId
        };

        UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenDateTimeEqualNowIsGiven_UpdateBookValidator_ShouldBeReturnError()
    {
        UpdateBookCommand command = new UpdateBookCommand(null);
        command.Model = new UpdateBookModel(){
            Title="Lotr",
            PageCount = 100,
            PublishDate = DateTime.Now.Date,
            GenreId = 1,
            AuthorId = 1
        };

        UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputsAreGiven_UpdateBookValidator_ShouldNotReturnError()
    {
        UpdateBookCommand command = new UpdateBookCommand(null);
        command.BookId = 1;
        command.Model = new UpdateBookModel(){
            Title="Lotr",
            PageCount = 100,
            PublishDate = DateTime.Now.Date.AddYears(-2),
            GenreId = 2,
            AuthorId = 1
        };

        UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().Be(0);
    }
}
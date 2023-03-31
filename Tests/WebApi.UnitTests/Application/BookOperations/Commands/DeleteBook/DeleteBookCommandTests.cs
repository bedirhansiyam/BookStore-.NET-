using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.DBOperations;
using WebApi.Entites;

namespace Application.BookOperations.Commands.DeleteBook;

public class DeleteBookCommandTests: IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _context;

    public DeleteBookCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
    }

    [Fact]
    public void WhenDoesNotExistBookIdIsGivenInDelete_InvalidOperationException_ShouldBeReturn()
    {
        var book = new Book(){Title = "Test_WhenDoesNotExistBookIdIsGiven_InvalidOperationException_ShouldBeReturn", PageCount = 100, PublishDate = new DateTime(1990,03,03), GenreId = 2, AuthorId=1};
        _context.Books.Add(book);
        _context.SaveChanges();

        DeleteBookCommand command = new DeleteBookCommand(_context);
        command.BookId = -1;

        FluentActions
            .Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("The book doesn't exist");
    }

    [Fact]
    public void WhenValidBookIdIsGiven_Book_ShouldBeDeleted()
    {
        var book = new Book(){Title = "Test_WhenInvalidBookIdIsGiven_InvalidOperationException_ShouldBeReturn", PageCount = 100, PublishDate = new DateTime(1990,03,03), GenreId = 2, AuthorId=1};
        _context.Books.Add(book);
        _context.SaveChanges();

        DeleteBookCommand command = new DeleteBookCommand(_context);
        command.BookId = book.Id;

        FluentActions.Invoking(() => command.Handle()).Invoke();

        var control = _context.Books.SingleOrDefault( x => x.Id == book.Id);
        control.Should().BeNull();
            
    }
}
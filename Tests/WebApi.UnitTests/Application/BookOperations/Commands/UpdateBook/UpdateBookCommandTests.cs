using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.DBOperations;
using WebApi.Entites;
using static WebApi.Application.BookOperations.Commands.UpdateBook.UpdateBookCommand;

namespace Application.BookOperations.Commands.UpdateBook;

public class UpdateBookCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _context;

    public UpdateBookCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
    }

    [Fact]
    public void WhenAlreadyExistBookTitleIsGivenInUpdate_InvalidOperationException_ShouldBeReturn()
    {
        var book1 = new Book(){Title = "Test1_WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn", PageCount = 100, PublishDate = new DateTime(1990,03,03), GenreId = 2, AuthorId=1};
        _context.Books.Add(book1);
        _context.SaveChanges();

        var book2 = new Book(){Title = "Test2_WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn", PageCount = 20, PublishDate = new DateTime(1990,03,03), GenreId = 1, AuthorId=2};
        _context.Books.Add(book2);
        _context.SaveChanges();

        UpdateBookCommand command = new UpdateBookCommand(_context);   
        command.BookId = book2.Id;
        command.Model = new UpdateBookModel(){Title = book1.Title};

        FluentActions
            .Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("The book name already exists.");
    }

    [Fact]
    public void WhenDoesNotExistBookIdIsGivenInUpdate_InvalidOperationException_ShouldBeReturn()
    {
        var book = new Book(){Title = "Test1_WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn", PageCount = 100, PublishDate = new DateTime(1990,03,03), GenreId = 2, AuthorId=1};
        _context.Books.Add(book);
        _context.SaveChanges();

        UpdateBookCommand command = new UpdateBookCommand(_context);
        command.BookId = 10;

        FluentActions
            .Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("The book doesn't exist.");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Book_ShouldBeUpdated()
    {
        var book = new Book(){Title = "Test1", PageCount = 100, PublishDate = new DateTime(1990,03,03), GenreId = 2, AuthorId=1};
        _context.Books.Add(book);
        _context.SaveChanges();

        UpdateBookCommand command = new UpdateBookCommand(_context);
        UpdateBookModel model = new UpdateBookModel(){Title="Hobbit", PageCount = 100, PublishDate = DateTime.Now.Date.AddYears(-2), GenreId =1, AuthorId =1};
        command.Model = model;
        command.BookId = book.Id;

        FluentActions.Invoking(() => command.Handle()).Invoke();

        book = _context.Books.SingleOrDefault(book => book.Title == model.Title);
        book.Should().NotBeNull();
        book.PageCount.Should().Be(model.PageCount);
        book.PublishDate.Should().Be(model.PublishDate);
        book.GenreId.Should().Be(model.GenreId);
        book.AuthorId.Should().Be(model.AuthorId);
    }
}
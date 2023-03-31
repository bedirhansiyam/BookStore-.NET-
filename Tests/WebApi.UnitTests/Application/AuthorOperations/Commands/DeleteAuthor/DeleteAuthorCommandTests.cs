using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands.DeleteAuthor;
using WebApi.DBOperations;
using WebApi.Entites;

namespace Application.AuthorOperations.Commands.DeleteAuthor;

public class DeleteAuthorCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _context;

    public DeleteAuthorCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
    }

    [Fact]
    public void WhenDoesNotExistAuthorIdIsGivenInDelete_InvalidOperationException_ShouldBeReturn()
    {
        var author = new Author(){Name = "James", Surname = "Joyce", BirthDate = new DateTime(1993,02,12)};
        _context.Authors.Add(author);
        _context.SaveChanges();

        DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
        command.AuthorId = 10;

        FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("The author doesn't exist.");
    }

    [Fact]
    public void WhenAuthorHasBooksIsGivenInDelete_InvalidOperationException_ShouldBeReturn()
    {
        var author = new Author(){Name = "James", Surname = "Joyce", BirthDate = new DateTime(1993,02,12)};        
        _context.Authors.Add(author);
        _context.SaveChanges();

        var book = new Book(){Title = "Test", PageCount = 100, PublishDate = new DateTime(1990,03,03), GenreId = 2, AuthorId=author.Id};              
        _context.Books.Add(book);
        _context.SaveChanges();

        author.Books = _context.Books.Where(x => x.AuthorId == author.Id).ToList();

        DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
        command.AuthorId = author.Id;

        FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("The author has book/s. To delete the author you must first delete the author's book/s. ");
    }

    [Fact]
    public void WhenValidInputsAreGivenInDelete_InvalidOperationException_ShouldNotBeReturn()
    {
        var author = new Author(){Name = "James", Surname = "Joyce", BirthDate = new DateTime(1993,02,12)};
        _context.Authors.Add(author);
        _context.SaveChanges();

        var book = new Book(){Title = "Test", PageCount = 100, PublishDate = new DateTime(1990,03,03), GenreId = 2, AuthorId=author.Id+1};              
        _context.Books.Add(book);
        _context.SaveChanges();

        author.Books = _context.Books.Where(x => x.AuthorId == author.Id).ToList();

        DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
        command.AuthorId = author.Id;

        FluentActions.Invoking(() => command.Handle()).Invoke();

        var result = _context.Authors.SingleOrDefault(x => x.Id == author.Id);
        result.Should().BeNull();
    }
}

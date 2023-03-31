using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.DBOperations;
using WebApi.Entites;

namespace Application.BookOperations.Queries.GetBookDetail;

public class GetBookDetailQueryTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public GetBookDetailQueryTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenGivenIdIsNotMatchAnyBook_InvalidOperationException_ShoulBeReturn()
    {
        var book = new Book(){Title = "Test_WhenGivenIdIsNotMatchAnyBook_InvalidOperationException_ShoulBeReturn", PageCount = 100, PublishDate = new DateTime(1990,03,03), GenreId = 2, AuthorId=1};
        _context.Books.Add(book);
        _context.SaveChanges();

        GetBookDetailQuery query = new GetBookDetailQuery(_context,_mapper);
        query.BookId = -1;

        FluentActions.Invoking(() => query.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("The book doesn't exist");
    }

    [Fact]
    public void WhenValidIdIsGiven_Book_ShouldBeGotten()
    {
        var book = new Book(){Title = "Test_WhenGivenIdIsNotMatchAnyBook_InvalidOperationException_ShoulBeReturn", PageCount = 100, PublishDate = new DateTime(1990,03,03), GenreId = 2, AuthorId=1};
        _context.Books.Add(book);
        _context.SaveChanges();

        GetBookDetailQuery query = new GetBookDetailQuery(_context,_mapper);
        query.BookId = book.Id;

        FluentActions.Invoking(() => query.Handle()).Invoke();

        var control = _context.Books.SingleOrDefault( x => x.Id == book.Id);
        control.Should().NotBeNull();
    }
}
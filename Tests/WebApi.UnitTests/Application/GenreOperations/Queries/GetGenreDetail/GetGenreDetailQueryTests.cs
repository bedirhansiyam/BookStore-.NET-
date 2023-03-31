using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.DBOperations;
using WebApi.Entites;

namespace Application.GenreOperations.Queries.GetGenreDetail;

public class GetGenreDetailQueryTests: IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public GetGenreDetailQueryTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenGivenIdIsNotMatchAnyGenre_InvalidOperationException_ShouldBeReturn()
    {
        var genre = new Genre(){Name = "Test_WhenGivenIdIsNotMatchAnyGenre_InvalidOperationException_ShouldBeReturn"};
        _context.Genres.Add(genre);
        _context.SaveChanges();

        GetGenreDetailQuery query = new GetGenreDetailQuery(_context,_mapper);
        query.GenreId = -1;

        FluentActions.Invoking(() => query.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("The genre doesn't exist.");
    }

    [Fact]
    public void WhenValidIdIsGiven_Genre_ShouldBeGotten()
    {
        var genre = new Genre(){Name = "Test_WhenValidIdIsGiven_Genre_ShouldBeGotten"};
        _context.Genres.Add(genre);
        _context.SaveChanges();

        GetGenreDetailQuery query = new GetGenreDetailQuery(_context,_mapper);
        query.GenreId = genre.Id;

        FluentActions.Invoking(() => query.Handle()).Invoke();

        var result = _context.Genres.SingleOrDefault(x => x.Id == genre.Id);
        result.Should().NotBeNull();
    }
}
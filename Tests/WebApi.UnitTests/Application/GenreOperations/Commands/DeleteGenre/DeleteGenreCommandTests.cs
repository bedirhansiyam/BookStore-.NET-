using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using WebApi.DBOperations;
using WebApi.Entites;

namespace Application.GenreOperations.Commands.DeleteGenre;

public class DeleteGenreCommandTests: IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _context;

    public DeleteGenreCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
    }

    [Fact]
    public void WhenGivenGenreIdIsNotMatchWithAnyGenre_InvalidOperationException_ShouldBeReturn()
    {
        var genre = new Genre(){Name = "Test_WhenGivenGenreIdIsNotMatchWithAnyGenre_InvalidOperationException_ShouldBeReturn"};
        _context.Genres.Add(genre);
        _context.SaveChanges();

        DeleteGenreCommand command = new DeleteGenreCommand(_context);
        command.GenreId = 10;

        FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("The genre doesn't exist.");
    }

    [Fact]
    public void WhenGivenGenreIdIsMatchWithAnyGenre_InvalidOperationException_ShouldNotBeReturn()
    {
        var genre = new Genre(){Name = "Test_WhenGivenGenreIdIsNotMatchWithAnyGenre_InvalidOperationException_ShouldBeReturn"};
        _context.Genres.Add(genre);
        _context.SaveChanges();

        DeleteGenreCommand command = new DeleteGenreCommand(_context);
        command.GenreId = genre.Id;

        FluentActions.Invoking(() => command.Handle()).Invoke();
        var control = _context.Genres.SingleOrDefault(x => x.Id == genre.Id);
        control.Should().BeNull();
    }
}
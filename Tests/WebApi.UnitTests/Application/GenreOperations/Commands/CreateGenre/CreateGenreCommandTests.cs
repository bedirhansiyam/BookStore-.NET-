using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.DBOperations;
using WebApi.Entites;

namespace Application.GenreOperations.Commands.CreateGenre;


public class CreateGenreCommandTests: IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _context;

    public CreateGenreCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
    }

    [Fact]
    public void WhenAlreadyExistGenreNameIsGiven_InvalidOperationException_ShouldBeReturn()
    {
        var genre = new Genre(){Name = "Test_WhenAlreadyExistGenreNameIsGiven_InvalidOperationException_ShouldBeReturn"};
        _context.Genres.Add(genre);
        _context.SaveChanges();

        CreateGenreCommand command = new CreateGenreCommand(_context);
        command.Model = new CreateGenreModel(){Name = genre.Name};

        FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Genre already exist.");
    }

    [Fact]
    public void WhenValidGenreNameIsGiven_Genre_ShouldBeCreated()
    {
        CreateGenreCommand command = new CreateGenreCommand(_context);
        CreateGenreModel model = new CreateGenreModel(){Name = "Test Genre"};
        command.Model = model;
        

        FluentActions.Invoking(() => command.Handle()).Invoke();

        var genre = _context.Genres.SingleOrDefault(x => x.Name == model.Name);
        genre.Should().NotBeNull();
    }
}
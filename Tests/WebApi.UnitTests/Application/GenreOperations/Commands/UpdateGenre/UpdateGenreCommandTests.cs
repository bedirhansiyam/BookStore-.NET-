using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.DBOperations;
using WebApi.Entites;

namespace Application.GenreOperations.Commands.UpdateGenre;

public class UpdateGenreCommandTests: IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _context;

    public UpdateGenreCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
    }

    [Fact]
    public void WhenAlreadyExistGenreNameIsGivenInUpdate_InvalidOperationException_ShouldBeReturn()
    {
        var genre1 = new Genre(){Name = "Test1_WhenAlreadyExistGenreNameIsGivenInUpdate_InvalidOperationException_ShouldBeReturn"};
        _context.Genres.Add(genre1);
        _context.SaveChanges();

        var genre2 = new Genre(){Name = "Test2_WhenAlreadyExistGenreNameIsGivenInUpdate_InvalidOperationException_ShouldBeReturn"};
        _context.Genres.Add(genre2);
        _context.SaveChanges();

        UpdateGenreCommand command = new UpdateGenreCommand(_context);
        command.GenreId = genre1.Id;
        command.Model = new UpdateGenreModel(){Name = genre2.Name};

        FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Genre name is already exist.");
    }

    [Fact]
    public void WhenGivenGenreIdNotMatchAnyGenre_InvalidOperationException_ShouldBeReturn()
    {
        var genre = new Genre(){Name = "Test_WhenGivenGenreIdNotMatchAnyGenre_InvalidOperationException_ShouldBeReturn"};
        _context.Genres.Add(genre);
        _context.SaveChanges();

        UpdateGenreCommand command = new UpdateGenreCommand(_context);
        command.GenreId = 10;

        FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("The genre doesn't exist.");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Genre_ShouldBeUpdated()
    {
        var genre = new Genre(){Name = "Test_WhenValidInputsAreGiven_Genre_ShouldBeUpdated"};
        _context.Genres.Add(genre);
        _context.SaveChanges();

        UpdateGenreCommand command = new UpdateGenreCommand(_context);
        UpdateGenreModel model = new UpdateGenreModel();
        model.Name = "updated genre name";
        model.IsActive = true;
        
        command.GenreId = genre.Id;
        command.Model = model;

        FluentActions.Invoking(() => command.Handle()).Invoke();

        genre = _context.Genres.SingleOrDefault(x => x.Name == genre.Name);
        genre.Should().NotBeNull();
        genre.Name.Should().Be(model.Name);
        genre.IsActive.Should().Be(true);
    }
}
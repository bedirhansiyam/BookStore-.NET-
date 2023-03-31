using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.DBOperations;
using WebApi.Entites;
using static WebApi.Application.AuthorOperations.Commands.UpdateAuthor.UpdateAuthorCommand;

namespace Application.AuthorOperations.Commands.UpdateAuthor;

public class UpdateAuthorCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _context;

    public UpdateAuthorCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
    }

    [Fact]
    public void WhenDoesNotExistAuthorIdIsGivenInUpdate_InvalidOperationException_ShouldBeReturn()
    {
        var author = new Author(){Name = "James", Surname = "Joyce", BirthDate = new DateTime(1993,02,12)};
        _context.Authors.Add(author);
        _context.SaveChanges();

        UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
        command.AuthorId = 10;

        FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("The author doesn't exist.");
    }

    [Fact]
    public void WhenValidAuthorIdIsGivenInUpdate_InvalidOperationException_ShouldNotReturn()
    {
        var author = new Author(){Name = "James", Surname = "Joyce", BirthDate = new DateTime(1993,02,12)};
        _context.Authors.Add(author);
        _context.SaveChanges();

        UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
        UpdateAuthorModel model = new UpdateAuthorModel(){Name = "Franz", Surname = "Kafka", BirthDate = new DateTime(1983,02,12)};
        command.Model = model;
        command.AuthorId = author.Id;

        FluentActions.Invoking(() => command.Handle()).Invoke();
        
        author = _context.Authors.SingleOrDefault(x => x.Id == author.Id);
        author.Should().NotBeNull();
    }
}
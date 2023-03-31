using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.DBOperations;
using WebApi.Entites;
using static WebApi.Application.AuthorOperations.Commands.CreateAuthor.CreateAuthorCommand;

namespace Application.AuthorOperations.Commands.CreateAuthor;

public class CreateAuthorCommandTests: IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public CreateAuthorCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenAlreadyExistAuthorFullnameIsGiven_InvalidOperationException_ShouldBeReturn()
    {
        var author = new Author(){Name = "AuthorName", Surname = "AuthorSurname", BirthDate = new DateTime(1993,02,13)};
        _context.Authors.Add(author);
        _context.SaveChanges();

        CreateAuthorCommand command = new CreateAuthorCommand(_context,_mapper);
        command.Model = new CreateAuthorModel(){Name = author.Name, Surname = author.Surname};

        FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("The author already exists.");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Author_ShouldBeCreated()
    {
        CreateAuthorCommand command = new CreateAuthorCommand(_context,_mapper);
        CreateAuthorModel model = new CreateAuthorModel(){Name = "AuthorName", Surname = "AuthorSurname", BirthDate = new DateTime(1993,02,12)};
        command.Model = model;

        FluentActions.Invoking(() => command.Handle()).Invoke();

        var author = _context.Authors.SingleOrDefault(author => author.Name == model.Name && author.Surname == model.Surname);
        author.Should().NotBeNull();
        author.BirthDate.Should().Be(model.BirthDate);
    }
}
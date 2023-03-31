using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.DBOperations;
using WebApi.Entites;
using static WebApi.Application.AuthorOperations.Queries.GetAuthorDetail.GetAuthorDetailQuery;

namespace Application.AuthorOperations.Queries.GetAuthorDetail;

public class GetAuthorDetailQueryTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public GetAuthorDetailQueryTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenGivenAuthorIdIsNotMatchAnyAuthor_InvalidOperationException_ShoulBeReturn()
    {
        var author = new Author(){Name = "name", Surname = "surname", BirthDate = DateTime.Now.Date.AddYears(-35)};
        _context.Authors.Add(author);
        _context.SaveChanges();

        GetAuthorDetailQuery query = new GetAuthorDetailQuery(_context,_mapper);
        query.AuthorId = -1;

        FluentActions.Invoking(() => query.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("The author doesn't exist.");
    }

    [Fact]
    public void WhenGivenAuthorIdIsMatchAnyAuthor_InvalidOperationException_ShoulNotBeReturn()
    {
        var author = new Author(){Name = "name", Surname = "surname", BirthDate = DateTime.Now.Date.AddYears(-35)};
        _context.Authors.Add(author);
        _context.SaveChanges();

        GetAuthorDetailQuery query = new GetAuthorDetailQuery(_context,_mapper);
        query.AuthorId = author.Id;

        FluentActions.Invoking(() => query.Handle()).Invoke();
        var control = _context.Authors.SingleOrDefault(x => x.Id == author.Id);
        control.Should().NotBeNull();
    }
}
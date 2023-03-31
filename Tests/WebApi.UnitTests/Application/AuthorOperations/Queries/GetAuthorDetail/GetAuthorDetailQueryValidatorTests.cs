using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;

namespace Application.AuthorOperations.Queries.GetAuthorDetail;

public class GetAuthorDetailQueryValidatorTests: IClassFixture<CommonTestFixture>
{
    [Fact]
    public void WhenInvalidAuthorIdIsGiven_Validator_ShouldBeReturnErrors()
    {
        GetAuthorDetailQuery query = new GetAuthorDetailQuery(null,null);
        query.AuthorId = -1;

        GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
        var result = validator.Validate(query);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidAuthorIdIsGiven_Validator_ShouldNotBeReturnErrors()
    {
        GetAuthorDetailQuery query = new GetAuthorDetailQuery(null,null);
        query.AuthorId = 1;

        GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
        var result = validator.Validate(query);

        result.Errors.Count.Should().Be(0);
    }
}
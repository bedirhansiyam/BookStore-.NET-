using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Queries.GetBookDetail;

namespace Application.BookOperations.Queries.GetBookDetail;

public class GetBookDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
{
    [Fact]
    public void WhenInvalidBookIdIsGiven_Validator_ShouldBeReturnError()
    {
        GetBookDetailQuery query = new GetBookDetailQuery(null,null);
        query.BookId = -1;
        GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();

        var result = validator.Validate(query);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidBookIdIsGiven_Validator_ShouldNotReturnError()
    {
        GetBookDetailQuery query = new GetBookDetailQuery(null,null);
        query.BookId = 1;
        GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();

        var result = validator.Validate(query);

        result.Errors.Count.Should().Be(0);
    }
}
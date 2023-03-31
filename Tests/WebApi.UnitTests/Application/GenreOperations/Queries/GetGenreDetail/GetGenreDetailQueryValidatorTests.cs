using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;

namespace Application.GenreOperations.Queries.GetGenreDetail;

public class GetGenreDetailQueryValidatorTests: IClassFixture<CommonTestFixture>
{
    [Fact]
    public void WhenInvalidGenreIdIsGiven_Validator_ShouldBeReturnError()
    {
        GetGenreDetailQuery query = new GetGenreDetailQuery(null,null);
        query.GenreId = -1;

        GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
        var result = validator.Validate(query);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidGenreIdIsGiven_Validator_ShouldNotReturnError()
    {
        GetGenreDetailQuery query = new GetGenreDetailQuery(null,null);
        query.GenreId = 1;

        GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
        var result = validator.Validate(query);

        result.Errors.Count.Should().Be(0);
    }
}
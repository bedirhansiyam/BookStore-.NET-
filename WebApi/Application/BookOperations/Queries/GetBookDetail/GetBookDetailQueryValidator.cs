namespace WebApi.Application.BookOperations.Queries.GetBookDetail;

using FluentValidation;

public class GetBookDetailQueryValidator : AbstractValidator<GetBookDetailQuery>
{
    public GetBookDetailQueryValidator()
    {
        RuleFor(command => command.BookId).GreaterThan(0);
    }
}
using FluentValidation;

namespace WebApi.BookOperations.GetBookId
{
    public class GetBookByIdValidator : AbstractValidator<GetBookByIdQuery>
    {
        public GetBookByIdValidator()
        {
            RuleFor(command => command.BookId).GreaterThan(0);
        }
    }
}
using FluentValidation;
using WebApi.Applications.AuthorOperations.Queries.GetAuthorDetails;

namespace WebApi.Applications.AuthorOperations.Queries.GetAuthors
{
    public class GetAuthorDetailsQueryValidator : AbstractValidator<GetAuthorDetailsQuery>
    {
        public GetAuthorDetailsQueryValidator()
        {
            RuleFor(query => query.AuthorID).GreaterThan(0);
        }
    }
}
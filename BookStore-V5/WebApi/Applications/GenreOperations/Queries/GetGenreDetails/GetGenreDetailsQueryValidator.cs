using FluentValidation;
using WepApi.Applications.GenreOperations.Queries.GetGenreDetails;

namespace WepApi.Applications.GenreOperations.Queries.GetGenres
{
    public class GetGenreDetailsQueryValidator : AbstractValidator<GetGenreDeteilsQuery>
    {
        public GetGenreDetailsQueryValidator()
        {
            RuleFor(query => query.GenreId).GreaterThan(0);
        }

    }
}
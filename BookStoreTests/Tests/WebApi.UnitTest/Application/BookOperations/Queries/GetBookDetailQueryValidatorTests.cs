using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.BookOperations.GetBookId;


namespace WebApi.Application.BookOperations.Commands.Queries
{
    public class GetBookDetailQueryValidatorTests : IClassFixture<CommanTestFixture>
    {
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(-10)]
        public void WhenInvalidIdIsGiven_Validator_ShouldBeReturn(int id)
        {
            GetBookByIdQuery query = new GetBookByIdQuery(null, null);
            query.BookId = id;

            GetBookByIdValidator validator = new GetBookByIdValidator();
            var result = validator.Validate(query);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

    }
}
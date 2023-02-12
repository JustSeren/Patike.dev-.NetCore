using FluentAssertions;
using TestSetup;
using WebApi.Applications.GenreOperations.DeleteGenre;
using WebApi.Applications.GenreOperations.UpdateGenre;
using WebApi.DBOperations;

namespace Application.BookOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandValidatorTest : IClassFixture<CommanTestFixture>
    {
        private readonly BookStoreDBContext _context;

        public UpdateGenreCommandValidatorTest(CommanTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Theory]
        [InlineData("as")]
        [InlineData("as ")]
        [InlineData(" a ")]
        [InlineData("asd")]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(string name)
        {
            //arrange
            UpdateGenreCommand command = new UpdateGenreCommand(null);
            command.Model = new UpdateGenreModel() { Name = name };

            //act
            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);

        }

        [InlineData("asdf")]
        [InlineData("asd dff")]
        [Theory]
        public void WhenInvalidInputsAreGiven_Validator_ShouldNotBeReturnErrors(string name)
        {
            UpdateGenreCommand command = new UpdateGenreCommand(null);
            command.Model = new UpdateGenreModel() { Name = name };

            UpdateGenreCommandValidator validations = new UpdateGenreCommandValidator();
            var result = validations.Validate(command);

            result.Errors.Count.Should().Be(0);
        }


    }
}
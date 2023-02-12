using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.BookOperations.CreateBook;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandValidatorTests : IClassFixture<CommanTestFixture>
    {
        [Theory] // veri kadar çalışıyor
        [InlineData("Lord of the Rings", 0, 0)]
        [InlineData("Lord of the Rings", 0, 1)]
        [InlineData("Lord of the Rings", 100, 0)]
        [InlineData("", 0, 0)]
        [InlineData("", 100, 0)]
        [InlineData("", 0, 1)]
        [InlineData("lor", 10, 1)]
        [InlineData("Lor", 100, 0)]
        [InlineData("Lor", 0, 1)]
        [InlineData(" ", 100, 1)]

        //şart-koşul-sonuç
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(string title, int pageCount, int genreId)
        {
            //arrange
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookModel()
            {
                Title = title,
                PageCount = pageCount,
                PublishDate = DateTime.Now.Date.AddYears(-1),
                GenreId = genreId
            };
            //act
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);

        }
        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookModel()
            {
                Title = "Lord of the Rings",
                PageCount = 1010,
                PublishDate = DateTime.Now.Date,
                GenreId = 2
            };

            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var error = validator.Validate(command);
            error.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnError()
        {
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookModel()
            {
                Title = "Lord of the Rings",
                PageCount = 1010,
                PublishDate = DateTime.Now.Date.AddYears(-2),
                GenreId = 2
            };

            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var error = validator.Validate(command);
            error.Errors.Count.Should().Be(0);
        }

    }
}
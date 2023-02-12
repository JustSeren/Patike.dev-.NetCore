
using FluentAssertions;
using TestSetup;
using WebApi.Applications.GenreOperations.DeleteGenre;
using WebApi.DBOperations;

namespace Application.BookOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandTests : IClassFixture<CommanTestFixture>
    {
        private readonly BookStoreDBContext _context;

        public DeleteGenreCommandTests(CommanTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenAlreadyExistGenreIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // Arrange (Hazırlık)
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = 100;

            // act & asset (Çalıştırma ve Doğrulama)
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap türü bulunamadı!");

        }

        [Fact]
        public void WhenValidInputsAreGiven_Genre_ShouldBeCreated()
        {
            //arrange
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = 1;

            //act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //assert
            var genre = _context.Genres.SingleOrDefault(x => x.Id == command.GenreId);
            genre.Should().BeNull();

        }
    }
}
using FluentAssertions;
using TestSetup;
using WebApi.Applications.GenreOperations.DeleteGenre;
using WebApi.Applications.GenreOperations.UpdateGenre;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Application.BookOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandTests : IClassFixture<CommanTestFixture>
    {
        private readonly BookStoreDBContext _context;

        public UpdateGenreCommandTests(CommanTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenAlreadyExistGenreIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // Arrange (Hazırlık)
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.GenreId = 0;

            // act & asset (Çalıştırma ve Doğrulama)
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap Türü Bulunamadı.");

        }

        [Fact]
        public void WhenGivenNameIsSameWithAnotherGenre_InvalidOperationException_ShouldBeReturn()
        {
            var genre = new Genre() { Name = "Romancee" };
            _context.Genres.Add(genre);
            _context.SaveChanges();

            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.GenreId = 2;
            command.Model = new UpdateGenreModel() { Name = "Romancee" };

            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Aynı isimli bir kitap türü zaten mevcut");
        }

        [Fact]
        public void WhenGivenBookIdinDB_Genre_ShouldBeUpdate()
        {
            UpdateGenreCommand command = new UpdateGenreCommand(_context);

            UpdateGenreModel model = new UpdateGenreModel() { Name = "WhenGivenBookIdinDB_Genre_ShouldBeUpdate" };
            command.Model = model;
            command.GenreId = 1;

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var genre = _context.Genres.SingleOrDefault(genre => genre.Id == command.GenreId);
            genre.Should().NotBeNull();

        }
    }
}
using FluentAssertions;
using TestSetup;
using WebApi.BookOperations.DeleteBook;
using WebApi.BookOperations.UpdateBook;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandTests : IClassFixture<CommanTestFixture>
    {
        private readonly BookStoreDBContext _context;

        public UpdateBookCommandTests(CommanTestFixture testFixture)
        {
            _context = testFixture.Context;
        }
        [Fact]
        public void WhereAlreadyExistBookIdIsGiven_InvalidOperationException_ShoulBeReturn()
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = 0;

            FluentActions
            .Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap yok");

        }

        [Fact]
        public void WhenGivenBookId_inDbBook_ShouldBeUpdate()
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);

            UpdateBookModel model = new UpdateBookModel() { Title = "WhenGivenBookIdinDB_Book_ShouldBeUpdate", GenreId = 2 };
            command.Model = model;
            command.BookId = 1;

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var book = _context.Books.SingleOrDefault(book => book.Id == command.BookId);
            book.Should().NotBeNull();
        }

    }
}
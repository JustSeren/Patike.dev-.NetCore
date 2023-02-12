using FluentAssertions;
using TestSetup;
using WebApi.BookOperations.DeleteBook;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.BookOperations.Commands.DeleteBook
{
    public class DeletBookCommandTests : IClassFixture<CommanTestFixture>
    {
        private readonly BookStoreDBContext _context;


        public DeletBookCommandTests(CommanTestFixture testFixture)
        {
            _context = testFixture.Context;

        }

        [Fact]
        public void WhenGivenIdIsNotValid_InvalidOperationException_ShouldBeReturn()
        {
            DeleteBookByIdCommand command = new DeleteBookByIdCommand(_context);

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap bulunamadı");

        }
        [Fact]
        public void WhenGivenValidId_Book_ShouldBeDeleted()
        {
            var book = new Book()
            {
                Title = "Lord of the Rings",
                PageCount = 1020,
                PublishDate = new DateTime(1990, 05, 20),
                GenreID = 2
            };
            _context.Add(book);
            _context.SaveChanges();

            DeleteBookByIdCommand command = new DeleteBookByIdCommand(_context);
            command.BookId = book.Id;

            FluentActions.Invoking(() => command.Handle()).Invoke();

            book = _context.Books.SingleOrDefault(x => x.Id == book.Id);
            book.Should().BeNull();

        }
    }
}
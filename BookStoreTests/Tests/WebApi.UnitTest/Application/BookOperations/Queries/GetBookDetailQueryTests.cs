using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.BookOperations.GetBookId;
using WebApi.BookOperations.GetBooks;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.BookOperations.Commands.Queries
{
    public class GetBookDetailQueryTests : IClassFixture<CommanTestFixture>
    {
        private readonly BookStoreDBContext _context;
        private readonly IMapper _mapper;
        public GetBookDetailQueryTests(CommanTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenGivenBookIdIsNotinDB_InvalidOperationException_ShouldBeReturn()
        {
            GetBookByIdQuery command = new GetBookByIdQuery(_context, _mapper);
            command.BookId = 0;


            FluentActions.Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should()
            .Be("Kitap bulunamadı...");
        }

        [Fact]
        public void WhenGivenBookIdIsinDB_InvalidOperationException_ShouldBeReturn()
        {
            GetBookByIdQuery command = new GetBookByIdQuery(_context, _mapper);
            command.BookId = 1;


            FluentActions.Invoking(() => command.Handle()).Invoke();

            var book = _context.Books.SingleOrDefault(book => book.Id == command.BookId);
            book.Should().NotBeNull();
        }


    }
}
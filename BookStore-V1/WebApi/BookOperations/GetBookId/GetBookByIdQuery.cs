using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperations.GetBookId
{
    public class GetBookByIdQuery

    {
        public int BookId { get; set; }
        private readonly BookStoreDBContext _dbContext;
        public GetBookByIdQuery(BookStoreDBContext dBContext)
        {
            _dbContext = dBContext;
        }
        public BookViewByIdModel Handle()
        {
            var book = _dbContext.Books.Where(x => x.Id == BookId).SingleOrDefault();

            if (book is null)
                throw new InvalidOperationException("Kitap Bulunamadı");


            BookViewByIdModel vm = new BookViewByIdModel();
            vm.Title = book.Title;
            vm.PageCount = book.PageCount;
            vm.PublishDate = book.PublishDate.Date.ToString("dd/MM/yy");
            vm.Genre = book.GenreID.ToString();
            return vm;
        }
    }

    public class BookViewByIdModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get; set; }

    }
}
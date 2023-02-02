using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperations.GetBooks
{
    public class GetBooksQuery
    {
        private readonly BookStoreDBContext _dbContext;
        private readonly IMapper _mapper;
        public GetBooksQuery(BookStoreDBContext dBContext, IMapper mapper)
        {
            _dbContext = dBContext;
            _mapper = mapper;
        }

        public List<BookViewModel> Handle()
        {
            var bookList = _dbContext.Books.OrderBy(x => x.Id).ToList<Book>();
            List<BookViewModel> vm = _mapper.Map<List<BookViewModel>>(bookList); //new List<BookViewModel>();
            // foreach (var book in bookList)
            // {
            //     vm.Add(new BookViewModel()
            //     {
            //         Title = book.Title,
            //         Genre = ((GenreEnum)book.GenreID).ToString(),
            //         PublishDate = book.PublishDate.Date.ToString("dd/MM/yy"),
            //         PageCount = book.PageCount
            //     });
            // }
            return vm;
        }
    }

    public class BookViewModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get; set; }

    }
}
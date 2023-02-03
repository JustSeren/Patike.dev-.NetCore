using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.DBOperations;
using WebApi.Entities;

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
            var bookList = _dbContext.Books.Include(x => x.Genre).OrderBy(x => x.Id).ToList<Book>();
            List<BookViewModel> vm = _mapper.Map<List<BookViewModel>>(bookList); //new List<BookViewModel>();

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
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperations.GetBookId
{
    public class GetBookByIdQuery

    {
        public int BookId { get; set; }
        private readonly BookStoreDBContext _dbContext;
        private readonly IMapper _mapper;
        public GetBookByIdQuery(BookStoreDBContext dBContext, IMapper mapper)
        {
            _dbContext = dBContext;
            _mapper = mapper;
        }
        public BookViewByIdModel Handle()
        {
            var book = _dbContext.Books.Include(x => x.Genre).Where(x => x.Id == BookId).SingleOrDefault();

            if (book is null)
                throw new InvalidOperationException("Kitap Bulunamadı");


            BookViewByIdModel vm = _mapper.Map<BookViewByIdModel>(book);
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
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperations.CreateBook
{
    public class CreateBookCommand
    {
        public CreateBookModel Model { get; set; }
        private readonly BookStoreDBContext _dbContext;
        private readonly IMapper _mapper;
        public CreateBookCommand(BookStoreDBContext dBContext, IMapper mapper)
        {
            _dbContext = dBContext;
            _mapper = mapper;
        }

        public void Hande()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Title == Model.Title);

            if (book is not null)
                throw new InvalidOperationException("Kitap zaten mevcut");

            book = _mapper.Map<Book>(Model);// new Book(); //Modelden gelen verileri book objesine convert et demek 
            // book.Title = Model.Title;
            // book.PublishDate = Model.PublishDate;
            // book.PageCount = Model.PageCount;
            // book.GenreID = Model.GenreId;

            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();
        }
    }

    public class CreateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
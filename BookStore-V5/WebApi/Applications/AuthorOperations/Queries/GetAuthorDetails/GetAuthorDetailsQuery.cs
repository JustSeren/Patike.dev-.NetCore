using System;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Applications.AuthorOperations.Queries.GetAuthorDetails
{
    public class GetAuthorDetailsQuery
    {
        public int AuthorID { get; set; }
        public readonly BookStoreDBContext _context;
        public readonly IMapper _mapper;
        public GetAuthorDetailsQuery(BookStoreDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public AuthorDetailViewModel Handle()
        {
            var author = _context.Authors.SingleOrDefault(x => x.Id == AuthorID);

            if (author is null)
                throw new InvalidOperationException("Kitap Yazarı Bulunamadı");

            return _mapper.Map<AuthorDetailViewModel>(author);
        }

    }

    public class AuthorDetailViewModel
    {
        public int Id { get; set; }
        public string AuthorName { get; set; }
        public string AuthorSurname { get; set; }
        public DateTime AuthorBD { get; set; }
        public string Book { get; set; }
    }
}
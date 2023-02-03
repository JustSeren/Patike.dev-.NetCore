using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Applications.AuthorOperations.Queries.GetAuthors
{
    public class GetAuthorsQuery
    {
        public readonly BookStoreDBContext _context;
        public readonly IMapper _mapper;
        public GetAuthorsQuery(BookStoreDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<AuthorsViewModel> Handle()
        {
            var author = _context.Authors.OrderBy(x => x.Id);
            List<AuthorsViewModel> returnObj = _mapper.Map<List<AuthorsViewModel>>(author);
            return returnObj;
        }
    }

    public class AuthorsViewModel
    {
        public int Id { get; set; }
        public string AuthorName { get; set; }
        public string AuthorSurname { get; set; }
        public DateTime AuthorBD { get; set; }
        public string Book { get; set; }
    }
}
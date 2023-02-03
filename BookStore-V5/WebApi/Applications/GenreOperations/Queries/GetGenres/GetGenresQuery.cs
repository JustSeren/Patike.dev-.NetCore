using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;

namespace WepApi.Applications.GenreOperations.Queries.GetGenres
{
    public class GetGenresQuery
    {
        public readonly BookStoreDBContext _context;
        public readonly IMapper _mapper;
        public GetGenresQuery(BookStoreDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<GenresViewModel> Handle()
        {
            var genre = _context.Genres.Where(x => x.IsActive).OrderBy(x => x.Id);
            List<GenresViewModel> returnObj = _mapper.Map<List<GenresViewModel>>(genre);
            return returnObj;
        }
    }

    public class GenresViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
}
using AutoMapper;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.GetBookId;
using WebApi.BookOperations.GetBooks;
using WebApi.Entities;
using WepApi.Applications.GenreOperations.Queries.GetGenreDetails;
using WepApi.Applications.GenreOperations.Queries.GetGenres;
using WebApi.Applications.AuthorOperations.Queries.GetAuthors;
using WebApi.Applications.AuthorOperations.Queries.GetAuthorDetails;

namespace WebApi.Common
{
    public class MapppingProfile : Profile
    {
        public MapppingProfile()
        {
            CreateMap<CreateBookModel, Book>();
            //createBookModel objesi book objesıne maplenebilir olsun demek 

            CreateMap<Book, BookViewByIdModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));
            //GetBookByIdQuery kısmı için 

            CreateMap<Book, BookViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));

            CreateMap<Genre, GenresViewModel>();
            CreateMap<Genre, GenreDetailViewModel>();

            CreateMap<Author, AuthorsViewModel>();
            CreateMap<Author, AuthorDetailViewModel>();

        }
    }
}
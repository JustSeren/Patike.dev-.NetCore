using AutoMapper;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.GetBookId;
using WebApi.BookOperations.GetBooks;

namespace WebApi.Common
{
    public class MapppingProfile : Profile
    {
        public MapppingProfile()
        {
            CreateMap<CreateBookModel, Book>();
            //createBookModel objesi book objesıne maplenebilir olsun demek 

            CreateMap<Book, BookViewByIdModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreID).ToString()));
            //GetBookByIdQuery kısmı için 

            CreateMap<Book, BookViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreID).ToString()));

        }
    }
}
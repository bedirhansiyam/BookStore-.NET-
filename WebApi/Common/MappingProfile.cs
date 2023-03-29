using AutoMapper;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.Entites;
using static WebApi.Application.BookOperations.Commands.CreateBook.CreateBookCommand;
using static WebApi.Application.BookOperations.Queries.GetBookDetail.GetBookDetailQuery;
using static WebApi.Application.BookOperations.Queries.GetBooks.GetBooksQuery;

namespace WebApi.Common;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateBookModel, Book>();
        CreateMap<Book, BookDetailViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));
        CreateMap<Book, BooksViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));
        CreateMap<Genre, GenresViewModel>();
        CreateMap<Genre, GenreDetailViewModel>();
    }
}
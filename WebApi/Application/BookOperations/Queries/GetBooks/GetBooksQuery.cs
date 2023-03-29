using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.DBOperations;
using WebApi.Entites;

namespace WebApi.Application.BookOperations.Queries.GetBooks;

public class GetBooksQuery
{
    private readonly IBookStoreDbContext _dbcontext;
    private readonly IMapper _mapper;

    public GetBooksQuery(IBookStoreDbContext dbContext, IMapper mapper)
    {
        _dbcontext = dbContext;
        _mapper = mapper;
    }

    public List<BooksViewModel> Handle()
    {
        var bookList = _dbcontext.Books.Include(x => x.Genre).Include(x => x.Author).OrderBy(x => x.Id)
            .ToList<Book>();
        List<BooksViewModel> vm = _mapper.Map<List<BooksViewModel>>(bookList);
        return vm;
    }

    public class BooksViewModel
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get; set; }
        
    }
}
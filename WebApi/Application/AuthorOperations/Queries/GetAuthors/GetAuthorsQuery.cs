using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entites;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthors;

public class GetAuthorsQuery
{
    private readonly IBookStoreDbContext _context;
    private readonly IMapper _mapper;

    public GetAuthorsQuery(IMapper mapper, IBookStoreDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public List<AuthorsViewModel> Handle()
    {
        var authorList = _context.Authors.OrderBy(x => x.Id).ToList<Author>();
        List<AuthorsViewModel> vm = _mapper.Map<List<AuthorsViewModel>>(authorList);
        return vm;
    }

    public class AuthorsViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string BirthDate { get; set; }
        public List<Book> Books { get; set; }
    }
}
using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entites;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;

public class GetAuthorDetailQuery
{
    public int AuthorId { get; set; }
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public GetAuthorDetailQuery(BookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public AuthorDetailViewModel Handle()
    {
        var author = _context.Authors.Where(x => x.Id == AuthorId).SingleOrDefault();
        if(author is null)
            throw new InvalidOperationException("The author doesn't exist.");

        AuthorDetailViewModel vm = _mapper.Map<AuthorDetailViewModel>(author);
        return vm;        
    }

    public class AuthorDetailViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string BirthDate { get; set; }
        public List<Book> Books { get; set; }
    }
    
}
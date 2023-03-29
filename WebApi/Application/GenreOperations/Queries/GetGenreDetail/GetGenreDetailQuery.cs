using System.Linq;
using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Application.GenreOperations.Queries.GetGenreDetail;

public class GetGenreDetailQuery
{
    public int GenreId;
    public readonly IBookStoreDbContext _context;
    public readonly IMapper _mapper;
    public GetGenreDetailQuery(IBookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public GenreDetailViewModel Handle()
    {
        var genre = _context.Genres.SingleOrDefault(x => x.IsActive && x.Id == GenreId);
        if(genre is null)
            throw new InvalidOperationException("The genre doesn't exist.");

        GenreDetailViewModel returnObj = _mapper.Map<GenreDetailViewModel>(genre);
        return returnObj;
    }
}

public class GenreDetailViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
}
using WebApi.DBOperations;
using WebApi.Entites;

namespace WebApi.Application.GenreOperations.Commands.CreateGenre;

public class CreateGenreCommand
{
    public CreateGenreModel Model{get; set;}
    private readonly BookStoreDbContext _context;

    public CreateGenreCommand(BookStoreDbContext context)
    {
        _context = context;
    }

    public void Handle()
    {
        var genre = _context.Genres.SingleOrDefault(x => x.Name == Model.Name);
        if(genre is not null)
            throw new InvalidOperationException("Genre is already exist.");

        genre = new Genre();
        genre.Name = Model.Name;
        _context.Add(genre);
        _context.SaveChanges();
    }
}

public class CreateGenreModel
{
    public string Name { get; set; }
}
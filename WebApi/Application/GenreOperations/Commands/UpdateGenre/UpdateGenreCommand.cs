using WebApi.DBOperations;

namespace WebApi.Application.GenreOperations.Commands.UpdateGenre;

public class UpdateGenreCommand
{
    public int GenreId { get; set; }
    private readonly BookStoreDbContext _context;
    public UpdateGenreModel Model { get; set; }

    public UpdateGenreCommand(BookStoreDbContext context)
    {
        _context = context;
    }

    public void Handle()
    {
        var genre = _context.Genres.SingleOrDefault(x => x.Id == GenreId);
        if(genre is null)
            throw new InvalidOperationException("The genre doesn't exist.");

        if(_context.Genres.Any(x => x.Name.ToLower() == Model.Name.ToLower() && x.Id != GenreId))
            throw new InvalidOperationException("Genre name is already exist.");

        genre.Name = string.IsNullOrEmpty(Model.Name.Trim()) ? genre.Name : Model.Name;
        genre.IsActive = Model.IsActive;
        _context.SaveChanges();
    }
}

public class UpdateGenreModel
{
    public string Name { get; set; }
    public bool IsActive  { get; set; } = true;
}
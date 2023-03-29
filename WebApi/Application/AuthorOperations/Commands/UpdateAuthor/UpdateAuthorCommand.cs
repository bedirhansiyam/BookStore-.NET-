using WebApi.DBOperations;
using WebApi.Entites;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor;

public class UpdateAuthorCommand
{
    private readonly BookStoreDbContext _context;
    public int AuthorId { get; set; }
    public UpdateAuthorModel Model { get; set; }
    public UpdateAuthorCommand(BookStoreDbContext context)
    {
        _context = context;
    }

    public void Handle()
    {
        var author = _context.Authors.SingleOrDefault(x => x.Id == AuthorId);
        if (author is null)
            throw new InvalidOperationException("The author doesn't exist.");

        author.Name = string.IsNullOrEmpty(Model.Name.Trim()) ? author.Name : Model.Name;
        author.Surname = string.IsNullOrEmpty(Model.Surname.Trim()) ? author.Surname : Model.Surname;
        author.BirthDate = Model.BirthDate != default ? author.BirthDate : Model.BirthDate;

        _context.SaveChanges();
    }

    public class UpdateAuthorModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
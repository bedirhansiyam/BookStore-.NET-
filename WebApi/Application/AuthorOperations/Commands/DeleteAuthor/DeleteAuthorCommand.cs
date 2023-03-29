using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Commands.DeleteAuthor;

public class DeleteAuthorCommand
{
    private readonly IBookStoreDbContext _context;
    public int AuthorId { get; set; }
    
    public DeleteAuthorCommand(IBookStoreDbContext context)
    {
        _context = context;
    }

    public void Handle()
    {
        var author = _context.Authors.SingleOrDefault(x => x.Id == AuthorId);
        if(author is null)
            throw new InvalidOperationException("The author doesn't exist.");

        var bookList = _context.Books.Where(x => x.AuthorId == AuthorId).Any();
        if(bookList)
            throw new InvalidOperationException("The author has book/s. To delete the author you must first delete the author's book/s. ");

        _context.Authors.Remove(author);
        _context.SaveChanges();
    }
}
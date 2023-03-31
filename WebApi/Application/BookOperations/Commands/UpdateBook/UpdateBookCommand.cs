using WebApi.DBOperations;

namespace WebApi.Application.BookOperations.Commands.UpdateBook;

public class UpdateBookCommand
{
    private readonly IBookStoreDbContext _dbContext;
    public UpdateBookModel Model { get; set; }
    public int BookId { get; set; }

    public UpdateBookCommand(IBookStoreDbContext dbContext)
    {
        _dbContext = dbContext;        
    }

    public void Handle()
    {
        var book = _dbContext.Books.SingleOrDefault(x => x.Id == BookId);
        if(book is null)
            throw new InvalidOperationException("The book doesn't exist.");

        var book2 = _dbContext.Books.SingleOrDefault(x => x.Title == Model.Title);
        if(book2 is not null)
            throw new InvalidOperationException("The book name already exists.");

        book.GenreId = Model.GenreId == default ? book.GenreId : Model.GenreId;
        book.AuthorId = Model.AuthorId == default ? book.AuthorId : Model.AuthorId;
        book.PageCount = Model.PageCount == default ? book.PageCount : Model.PageCount;
        book.PublishDate = Model.PublishDate.Date == DateTime.Now.Date ? book.PublishDate : Model.PublishDate;
        book.Title = Model.Title == default ? book.Title : Model.Title;        

        _dbContext.SaveChanges();
    }

    public class UpdateBookModel
    {
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; } 
    }
}
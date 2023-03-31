using WebApi.DBOperations;
using WebApi.Entites;

namespace TestSetup;

public static class Books
{
    public static void AddBooks(this BookStoreDbContext context)
    {
        context.Books.AddRange(    
            new Book
            {
                //Id = 1,
                Title = "Ulysses",
                AuthorId = 1,
                GenreId = 3,
                PageCount = 732,
                PublishDate = new DateTime(1920,12,12)
            },
            new Book
            {
                //Id = 2,
                Title = "1Q84",
                AuthorId = 3,
                GenreId = 1,
                PageCount = 928,
                PublishDate = new DateTime(2009,05,29)
            },
            new Book
            {
                //Id = 3,
                Title = "Pride and Prejudice",
                AuthorId = 2,
                GenreId = 3,
                PageCount = 538,
                PublishDate = new DateTime(1813,01,28)
            }); 
    }
}
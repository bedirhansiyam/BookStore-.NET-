using Microsoft.EntityFrameworkCore;
using WebApi.Entites;

namespace WebApi.DBOperations;

public class DataGenerator
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
        {
            if(context.Books.Any())
            {
                return;
            }
            else
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
                    }
            );
            context.Genres.AddRange(
                    new Genre{
                        Name ="Alternate history",
                    },
                    new Genre{
                        Name = "Science Fiction",
                    },
                    new Genre{
                        Name = "Romance",
                    }
                );
            context.Authors.AddRange(
                new Author{
                    Name = "James",
                    Surname = "Joyce",
                    BirthDate = new DateTime(1882,02,02),
                    Books = context.Books.Where(x => x.AuthorId == 1).ToList()                
                },
                new Author{
                    Name = "Jane",
                    Surname = "Austen",
                    BirthDate = new DateTime(1775,12,16),
                    Books = context.Books.Where(x => x.AuthorId == 2).ToList()                      
                },
                new Author{
                    Name = "Haruki",
                    Surname = "Murakami",
                    BirthDate = new DateTime(1949,01,12),
                    Books = context.Books.Where(x => x.AuthorId == 3).ToList()                      
                }
            );
            }          
            
            context.SaveChanges();
        }
    }
}
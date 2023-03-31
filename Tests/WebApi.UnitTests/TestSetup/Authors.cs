using WebApi.DBOperations;
using WebApi.Entites;

namespace TestSetup;

public static class Authors
{
    public static void AddAuthors(this BookStoreDbContext context)
    {
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
            });
    }
}
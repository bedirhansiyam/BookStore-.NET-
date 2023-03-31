using WebApi.DBOperations;
using WebApi.Entites;

namespace TestSetup;

public static class Genres
{
    public static void AddGenres(this BookStoreDbContext context)
    {
        context.Genres.AddRange(
            new Genre{
                Name ="Alternate history",
            },
            new Genre{
                Name = "Science Fiction",
            },
            new Genre{
                Name = "Romance",
            });
    }
}
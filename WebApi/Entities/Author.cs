using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entites;

public class Author
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime BirthDate { get; set; }
    public List<Book> Books { get; set; }
}
namespace WebApi.TokenOperations.Models;

public class Token
{
    public string AccesToken { get; set; }
    public DateTime ExpirationDate { get; set; }
    public string RefreshToken { get; set; }
}
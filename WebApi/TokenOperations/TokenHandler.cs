using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using WebApi.Entites;
using WebApi.TokenOperations.Models;

namespace WebApi.TokenOperations;

public class TokenHandler
{
    private readonly IConfiguration _configuration;

    public TokenHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Token CreateAccessToken(User user)
    {
        Token tokenModel = new Token();
        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"])); 

        SigningCredentials signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        tokenModel.ExpirationDate = DateTime.Now.AddMinutes(15);

        JwtSecurityToken securityToken = new JwtSecurityToken(
            issuer:_configuration["Token:Issuer"],
            audience:_configuration["Token:Audience"],
            expires: tokenModel.ExpirationDate,
            notBefore: DateTime.Now,
            signingCredentials: signingCredentials
        );

        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

        tokenModel.AccesToken = tokenHandler.WriteToken(securityToken);
        tokenModel.RefreshToken = CreateRefreshToken();

        return tokenModel;
    }
    public string CreateRefreshToken()
    {
        return Guid.NewGuid().ToString();
    }
}
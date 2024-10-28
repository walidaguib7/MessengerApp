using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Messenger.Models;
using Messenger.Services;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Messenger.Repositories;

public class TokenService : IToken
{
    private readonly IConfiguration config;
    private readonly SymmetricSecurityKey key;

    public TokenService(IConfiguration configuration)
    {
        config = configuration;
        key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:SignInKey"]));
    }

    public string CreateToken(User user)
    {
        
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Email, user.Email),
            new(JwtRegisteredClaimNames.GivenName, user.UserName),
        };

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddMonths(2),
            SigningCredentials = credentials,
            Issuer = config["JWT:Issuer"],
            Audience = config["JWT:Audience"],
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var tk = tokenHandler.CreateToken(token);
        return tokenHandler.WriteToken(tk);
    }
}
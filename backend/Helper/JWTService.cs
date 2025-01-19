using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.IdentityModel.Tokens;

namespace Moodie.Helper;

public class JWTService
{
    private readonly string _secureKey;
    public JWTService(IConfiguration configuration)
    {
        _secureKey = configuration["JwtSettings:SecureKey"];
    }

    public string Generate(int id)
    {
        var SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secureKey));
        var credentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);
        var header = new JwtHeader(credentials);
        var payload = new JwtPayload(id.ToString(), null, null, null, DateTime.Today.AddDays(1));
        var token = new JwtSecurityToken(header, payload);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public JwtSecurityToken Verify(string jwt)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_secureKey);
        tokenHandler.ValidateToken(jwt,
            new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(key), ValidateIssuerSigningKey = true,
                ValidateIssuer = false, ValidateAudience = false
            }, out var validatedToken);
        return (JwtSecurityToken)validatedToken;
    }
}
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace auth.Helper
{
    public class JWTService
    {
        private string secureKey="this is a secure key";
        public string Generate(int id)
        {
         var SecurityKey= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secureKey));
         var credentials=new SigningCredentials(SecurityKey,SecurityAlgorithms.HmacSha256);
         var header=new JwtHeader(credentials);
         var payload = new JwtPayload(id.ToString(),null,null,null,DateTime.Today.AddDays(1)) ;
         var token=new JwtSecurityToken(header,payload);
         return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public JwtSecurityToken Verify(string jwt)
        {
            var tokenHandler =new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secureKey);
            tokenHandler.ValidateToken(jwt,new TokenValidationParameters{IssuerSigningKey = new SymmetricSecurityKey(key),ValidateIssuerSigningKey = true,ValidateIssuer = false,ValidateAudience = false},out SecurityToken validatedToken);
            return (JwtSecurityToken) validatedToken;
        }
    }
}

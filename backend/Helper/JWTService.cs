using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
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
    
    
    // Use GMails SMTP server to send emails
    
    public class EmailService
    {
        public void SendVerificationEmail(string email, string token)
        {
            if (!IsValidEmail(email))
            {
                // Handle the case where the email is invalid
                Console.WriteLine("Invalid email address format.");
                return;
            }

            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("moodieappfit@gmail.com", "xowmecvcskwbcifa"),
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network
            };

            var message = new MailMessage("moodieappfit@gmail.com", email)
            {
                Subject = "Email verification",
                Body = $"<a href='http://localhost:8000/api/verifyEmail?token={token}'>Verify email</a>",
                IsBodyHtml = true
            };

            try
            {
                client.Send(message);
            }
            catch (Exception ex)
            {
                // Handle any exceptions that might occur during sending
                Console.WriteLine($"Error sending email: {ex.Message}");
            }
        }

        public void SendTwoFactorCode(string email, string code)
        {
            if (!IsValidEmail(email))
            {
                // Handle the case where the email is invalid
                Console.WriteLine("Invalid email address format.");
                return;
            }

            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("moodieappfit@gmail.com", "xowmecvcskwbcifa"),
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network
            };

            var message = new MailMessage("moodieappfit@gmail.com", email)
            {
                Subject = "Two factor code",
                Body = $"Your two factor code is: {code}",
                IsBodyHtml = true
            };

            try
            {
                client.Send(message);
            }
            catch (Exception ex)
            {
                // Handle any exceptions that might occur during sending
                Console.WriteLine($"Error sending email: {ex.Message}");
            }

        }
        
        private bool IsValidEmail(string email)
        {
            // Regular expression pattern for email validation
            string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            return Regex.IsMatch(email, pattern);
        }
    }
}

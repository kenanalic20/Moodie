using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Moodie.Helper;

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
            Body = $"<a href='https://localhost:8000/api/verifyEmail?token={token}'>Verify email</a>",
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

    public void SendHabitMissedEmail(string email, string habitName)
    {
        if (!IsValidEmail(email)) return;

        var client = new SmtpClient("smtp.gmail.com", 587)
        {
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential("moodieappfit@gmail.com", "xowmecvcskwbcifa"),
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network
        };

        var message = new MailMessage("moodieappfit@gmail.com", email)
        {
            Subject = "Habit Streak Reset",
            Body = $"You missed your daily check-in for habit: {habitName}. Your streak has been reset to 0.",
            IsBodyHtml = true
        };

        try
        {
            client.Send(message);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending email: {ex.Message}");
        }
    }

    private bool IsValidEmail(string email)
    {
        // Regular expression pattern for email validation
        var pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
        return Regex.IsMatch(email, pattern);
    }
}

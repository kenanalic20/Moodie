using Moodie.Dtos;
using Moodie.Helper;
using Moodie.Models;
using Microsoft.AspNetCore.Mvc;
using Moodie.Interfaces;
namespace Moodie.Controllers;

[Route("api")]
[ApiController]
public class AuthController : Controller
{
    private readonly EmailService _emailService;
    private readonly JWTService _jwtService;
    private readonly IUserRepo _repositoryUser;
    private readonly AuthHelper _authHelper;
    private readonly ISettingsRepo _repositorySettings;

    public AuthController(IUserRepo repositoryUser, JWTService jwtService, EmailService emailService, AuthHelper authHelper, ISettingsRepo repositorySettings)
    {
        _repositoryUser = repositoryUser;
        _jwtService = jwtService;
        _emailService = emailService;
        _authHelper = authHelper;
        _repositorySettings = repositorySettings;
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterDto Dto)
    {
        var user = new User
        {
            Username = Dto.Username,
            Email = Dto.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(Dto.Password),
            EmailToken = Guid.NewGuid().ToString()
        };

        var exists = _repositoryUser.GetByEmail(user.Email);
        var existsUsername = _repositoryUser.GetByUsername(user.Username);
        if(existsUsername != null) return BadRequest("Username already exists");
        if (exists != null) return BadRequest("Email already exists");

        _emailService.SendVerificationEmail(user.Email, user.EmailToken);
        return Created("success", _repositoryUser.Create(user));
    }

    [HttpPost("login")]
    public IActionResult Login(LoginDto Dto)
    {
        var user = _repositoryUser.GetByEmail(Dto.Email);

        if (user == null) return NotFound("Invalid credentials");

        if (!BCrypt.Net.BCrypt.Verify(Dto.Password, user.Password)) return Unauthorized("Invalid credentials");

        if (!user.IsVerifiedEmail) return BadRequest("Email not verified. Please check your email for verification link.");

        var settings = _repositorySettings.GetByUserId(user.Id);
        bool twoFactorEnabled = settings != null && settings.TwoFactorEnabled;

        if (!twoFactorEnabled)
        {
            SetAuthCookie(user.Id);
            return Ok(new { message = "Login successful" });
        }

        if (string.IsNullOrEmpty(Dto.TwoStepCode))
        {
            var code = new Random().Next(100000, 999999);
            _emailService.SendTwoFactorCode(user.Email, code.ToString());

            user.EmailTwoStepToken = code.ToString();
            _repositoryUser.Update(user);

            return Ok(new { message = "Verification code sent to your email" });
        }

        if (user.EmailTwoStepToken == null || Dto.TwoStepCode != user.EmailTwoStepToken)
        {
            return Unauthorized("Invalid verification code");
        }

        SetAuthCookie(user.Id);

        user.EmailTwoStepToken = null;
        _repositoryUser.Update(user);

        return Ok(new { message = "Login successful" });
    }

    private void SetAuthCookie(int userId)
    {
        var jwt = _jwtService.Generate(userId);
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None
        };
        Response.Cookies.Append("jwt", jwt, cookieOptions);
    }

    [HttpGet("user")]
    public IActionResult User()
    {
        if (!_authHelper.IsUserLoggedIn(Request, out var userId)) return Unauthorized("Invalid or expired token.");

        var user = _repositoryUser.GetById(userId);

        return Ok(user);
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("jwt");
        return Ok(new { message = "success" });
    }

    [HttpGet("verifyEmail")]
    public IActionResult VerifyEmail(string token)
    {
        var user = _repositoryUser.GetByEmailToken(token);
        if (user == null) return NotFound("User not found");

        if (user.IsVerifiedEmail) return Redirect($"http://localhost:4200/login?email={user.Email}");

        user.IsVerifiedEmail = true;

        _repositoryUser.Update(user);
        return Redirect($"http://localhost:4200/login?email={user.Email}");
    }

    [HttpPost("request-reset-password")]
    public IActionResult RequestResetPassword(RequestResetPasswordDto Dto)
    {
        if (string.IsNullOrEmpty(Dto.Email))
            return BadRequest("Please insert email");

        var user = _repositoryUser.GetByEmail(Dto.Email);

        if (user == null)
            return NotFound("User not found");

        var resetToken = _jwtService.GeneratePasswordResetToken(user.Id);

        user.PasswordResetToken = resetToken;
        user.PasswordResetTokenExpiry = DateTime.UtcNow.AddHours(1);
        _repositoryUser.Update(user);

        _emailService.SendResetPasswordEmail(user.Email, resetToken);

        return Ok(new { message = "Password reset link sent to your email" });
    }

    [HttpPost("reset-password")]
    public IActionResult ResetPassword(ResetPasswordDto Dto)
    {
        if (!_jwtService.ValidatePasswordResetToken(Dto.Token, out var userId))
        {
            return BadRequest("Invalid or expired token");
        }

        var user = _repositoryUser.GetById(userId);
        if (user == null) return NotFound("User not found");

        user.Password = BCrypt.Net.BCrypt.HashPassword(Dto.NewPassword);
        user.PasswordResetToken = null;
        user.PasswordResetTokenExpiry = null;
        _repositoryUser.Update(user);

        return Ok(new { message = "Password reset successful" });
    }

}

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
    private readonly IUserRepo _repository;
    private readonly AuthHelper _authHelper;
    private readonly ISettingsRepo _repositorySettings;

    public AuthController(IUserRepo repository, JWTService jwtService, EmailService emailService, AuthHelper authHelper, ISettingsRepo repositorySettings)
    {
        _repository = repository;
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

        var exists = _repository.GetByEmail(user.Email);
        if (exists != null) return BadRequest("Email already exists");

        _emailService.SendVerificationEmail(user.Email, user.EmailToken);
        return Created("success", _repository.Create(user));
    }

    [HttpPost("login")]
    public IActionResult Login(LoginDto Dto)
    {
        var user = _repository.GetByEmail(Dto.Email);
        
        if (user == null) return NotFound("Invalid credentials");

        if (!BCrypt.Net.BCrypt.Verify(Dto.Password, user.Password)) return Unauthorized("Invalid credentials");

        // Check if 2FA is enabled for this user
        var settings = _repositorySettings.GetByUserId(user.Id);
        bool twoFactorEnabled = settings != null && settings.TwoFactorEnabled;

        // If 2FA is disabled, skip verification code
        if (!twoFactorEnabled)
        {
            // Skip 2FA process and authenticate user directly
            SetAuthCookie(user.Id);
            return Ok(new { message = "Login successful" });
        }

        // Handle 2FA verification
        if (string.IsNullOrEmpty(Dto.TwoStepCode))
        {
            // First step: Send verification code to email
            var code = new Random().Next(100000, 999999);
            _emailService.SendTwoFactorCode(user.Email, code.ToString());

            // Store the verification code in the user record
            user.EmailTwoStepToken = code.ToString();
            _repository.Update(user);

            return Ok(new { message = "Verification code sent to your email" });
        }

        // Second step: Verify the verification code
        if (user.EmailTwoStepToken == null || Dto.TwoStepCode != user.EmailTwoStepToken)
        {
            return Unauthorized("Invalid verification code");
        }

        // Set authentication cookie
        SetAuthCookie(user.Id);

        // Clear the verification code in the user record
        user.EmailTwoStepToken = null;
        _repository.Update(user);

        return Ok(new { message = "Login successful" });
    }

    // Helper method to set authentication cookie
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
        if(!_authHelper.IsUserLoggedIn(Request, out var userId)) return Unauthorized("Invalid or expired token.");
        
        var user = _repository.GetById(userId);

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
        var user = _repository.GetByEmailToken(token);
        if (user == null) return NotFound("User not found");

        if (user.IsVerifiedEmail) return BadRequest("Email already verified");

        user.IsVerifiedEmail = true;

        _repository.Update(user);
        //will need to change this
        return Redirect("http://localhost:4200/emailVerified");
    }
}

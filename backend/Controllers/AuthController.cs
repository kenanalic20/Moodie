﻿using auth.Dtos;
using auth.Helper;
using auth.Models;
using Microsoft.AspNetCore.Mvc;
using Moodie.Data;

namespace Moodie.Controllers;

[Route("api")]
[ApiController]
public class AuthController : Controller
{
    private readonly EmailService _emailService;
    private readonly JWTService _jwtService;
    private readonly IUserRepo _repository;

    public AuthController(IUserRepo repository, JWTService jwtService, EmailService emailService)
    {
        _repository = repository;
        _jwtService = jwtService;
        _emailService = emailService;
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

        if (user.EmailTwoStepToken?.Length == null)
        {
            var code = new Random().Next(100000, 999999);
            _emailService.SendTwoFactorCode(user.Email, code.ToString());

            user.EmailTwoStepToken = code.ToString();
            _repository.Update(user);
            return Ok(new { message = "Check your email for two factor code" });
        }

        if (Dto.TwoStepCode != user.EmailTwoStepToken) return Unauthorized("Invalid two factor code");

        var jwt = _jwtService.Generate(user.Id);
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = Request.IsHttps, // Set to true for HTTPS requests, false for HTTP requests
            SameSite = SameSiteMode.None
        };
        Response.Cookies.Append("jwt", jwt, cookieOptions);

        user.EmailTwoStepToken = null;
        _repository.Update(user);
        return Ok(new { message = "success" });
    }

    [HttpGet("user")]
    public IActionResult User()
    {
        try
        {
            var jwt = Request.Cookies["jwt"];
            var token = _jwtService.Verify(jwt);
            var userId = int.Parse(token.Issuer);
            var user = _repository.GetById(userId);
            return Ok(user);
        }
        catch (Exception e)
        {
            return Unauthorized("Invalid or expired credentials.");
        }
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

        return Redirect("http://localhost:3000/emailVerified");
    }
}
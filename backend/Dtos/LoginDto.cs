﻿namespace Moodie.Dtos;

public class LoginDto
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string? TwoStepCode { get; set; }
}
﻿namespace ScriptShoesCQRS.Models.Users;

public class CreateUserDto
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
}
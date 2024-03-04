using System;

namespace LogIn.Model;

public class UserModel
{

    private string Email { get; set; } = string.Empty;
    public String Username { get; set; } = string.Empty;
    private string Password { get; set; } = string.Empty;

    public UserModel(string email, string password, string username)
	{
		this.Email = email;
		this.Username = username;
		this.Password = password;
	}

    public UserModel(string username)
    {
        this.Username = username;
    }

    public UserModel()
    {
    }



}



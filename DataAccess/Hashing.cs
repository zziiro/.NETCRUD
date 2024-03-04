using System;
using System.Security.Cryptography;
using System.Text;

namespace LogIn.DataAccess;

public class Hashing
{
	public string passwordHash(string password)
	{
		using(SHA256 sha256 = SHA256.Create())
		{
			// convert user password to bytes
			byte[] passwordBytes = sha256.ComputeHash(
				Encoding.UTF8.GetBytes(password));

			// convert the bytes into a hash string
			string hashedPassword = BitConverter.ToString(
				passwordBytes).Replace("-", "").ToLower();

			// return that hash string
			return hashedPassword;
		}
	}
}



using System.Security.Cryptography;
using System.Text;
using LogIn.Model;
using LogIn.Pages;
using Npgsql;

namespace LogIn.DataAccess;

public class DatabaseAccess : ConnectionString
{

	private readonly Hashing hashing = new Hashing();


	// Register new user account method
	public void registerUser(string email, string username, string password)
	{

		// password hashing
		string hashedPassword = hashing.passwordHash(password);

		// create a postgres connection
		using(var postgresConnection = new NpgsqlConnection(postgresConnectionString))
		{
			// 'create' query
			var createQuery =
				"INSERT INTO users (email, username, password) " +
				"VALUES (@value1, @value2, @value3)";
			
			// create a new NpgsqlCommand
			using (var command = new NpgsqlCommand(createQuery, postgresConnection))
			{

				// add user data to the query
				command.Parameters.AddWithValue("value1", email);
				command.Parameters.AddWithValue("value2", username);
				command.Parameters.AddWithValue("value3", hashedPassword);

				try
				{
                    // open database connection
                    postgresConnection.Open();

					// excute the query
					command.ExecuteNonQuery();

					// close the postgres connection
					postgresConnection.Close();
                }catch(Exception ex)
				{
					Console.WriteLine("Error attempting to create account..");
					Console.WriteLine("[ERROR] - " + ex.ToString());
				}

            }
		}
	}


	// show all users method
	public List<string> AllUsers()
	{
		// method variable allUsers
		List<string> allUsers = new List<string>();

		// create a postgres connection
		using(var postgresConnection = new NpgsqlConnection(postgresConnectionString))
		{
			// create 'read' query
			var readQuery = "SELECT username FROM users";

			// create a new npgsql command
			using(var command = new NpgsqlCommand(readQuery, postgresConnection))
			{
				try
				{
					// open the postgres connection
					postgresConnection.Open();

					// execute reader command
					using(var reader = command.ExecuteReader())
					{
						while(reader.Read())
						{
							// add username to the list
							allUsers.Add(reader.GetString(0));
						}
                    }

					// close connection
					postgresConnection.Close();
                }
                catch(Exception ex)
				{
					Console.WriteLine("Error fetching all users..");
					Console.WriteLine("[ERROR] - " + ex.ToString());
				}
			}
		}
		// return the users list
		return allUsers;
	}

	// search useres by username method
	public List<string> searchByUsername(string username)
	{
		List<string> users = new List<string>();

		// create a new postgres connection
		using(var postgresConnection = new NpgsqlConnection(postgresConnectionString))
		{
			// create the search query
			var userSearchQuery =
				"SELECT username FROM users WHERE username=(@value)";

			// create a new npgsql command
			using(var command = new NpgsqlCommand(userSearchQuery, postgresConnection))
			{
				try
				{

					// open connection
					postgresConnection.Open();

					// add parameters to query
					command.Parameters.AddWithValue("value", username);

					// execute reader
					using (var reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							users.Add(reader.GetString(0));
						}
					}

					// close connection
					postgresConnection.Close();
				}catch(Exception ex)
				{
					Console.WriteLine("User does not exist..");
					Console.WriteLine("[ERROR] - " + ex.ToString());
				}
			}
		}

		return users;
	}

	// Delete Account
	public void deleteAccount(
		string accountToBeDeletedUsername, string accountToBeDeletedPassword)
	{

		// send password to hashfunction and return result
		string hashedPassword = hashing.passwordHash(accountToBeDeletedPassword);

		// create a new npgsql connection
		using(var postgresConnection = new NpgsqlConnection(postgresConnectionString))
		{
			// create the delete query
			var deleteAccountQuery =
				"DELETE FROM users WHERE " +
				"username=(@value1) AND password=(@value2)";

			// create a new command
			using(var command = new NpgsqlCommand(deleteAccountQuery, postgresConnection))
			{
				try
				{
					// open postgres connection
					postgresConnection.Open();

					// add query parameters
					command.Parameters.AddWithValue("value1", accountToBeDeletedUsername);
                    command.Parameters.AddWithValue("value2", hashedPassword);

                    // execute the command
                    command.ExecuteNonQuery();

					// close connection
					postgresConnection.Close();
				}catch(Exception ex)
				{
					Console.WriteLine("Unable to delete Account..");
					Console.WriteLine("[ERROR] - " + ex.ToString());
				}
			}
		}
	}

	// Update user account username
	public void updateUsername(
		string oldUsername, string updatedUsername, string password)
	{
		// send password through the hashing function
		string hashPassword = hashing.passwordHash(password);

		// new postgres connection
		using (var postgresConnection = new NpgsqlConnection(postgresConnectionString))
		{
			// create 'update' query
			var updateUsernameQuery =
				"UPDATE users SET username=(@value1) " +
				"WHERE username=(@value2) " +
				"AND password=(@value3)";

			// create new npgsql command
			using(var command = new NpgsqlCommand(updateUsernameQuery, postgresConnection))
			{
				try
				{
                    // open connection
                    postgresConnection.Open();

                    // add parameters to the query
                    command.Parameters.AddWithValue("value1", updatedUsername);
                    command.Parameters.AddWithValue("value2", oldUsername);
                    command.Parameters.AddWithValue("value3", hashPassword);

                    // execute query
                    command.ExecuteNonQuery();

                    // close connection
                    postgresConnection.Close();
                }catch(Exception ex)
				{
					Console.WriteLine("Could not update username..");
					Console.WriteLine("[ERROR] - " + ex.ToString());
				}
				
            }
		}
	}

	// update user account password
	public void updatePassword(string oldPassword, string updatedPassword)
	{
		// send the password through the hashing function
		string oldPasswordHash = hashing.passwordHash(oldPassword);
		string updatedPasswordHash = hashing.passwordHash(updatedPassword);

		// create postgres connection
		using (var postgresConnection = new NpgsqlConnection(postgresConnectionString))
		{
			// create 'update' query
			var updatePasswordQuery =
				"UPDATE users SET password=(@value1) " +
				"WHERE password=(@value2) ";

			// create new npgsql command
			using(var command = new NpgsqlCommand(updatePasswordQuery, postgresConnection))
			{
				// open connection
				postgresConnection.Open();

				// add parameters to query
				command.Parameters.AddWithValue("value1", updatedPasswordHash);
                command.Parameters.AddWithValue("value2", oldPasswordHash);

				// execute query
				command.ExecuteNonQuery();

				// close connection
				postgresConnection.Close();
            }
		}
	}
}




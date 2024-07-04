using Auth.Domain;
using Microsoft.Data.SqlClient;

namespace Auth
{
    internal class SQLServerUserRepository : IUserRepository
    {
        private readonly string connectionString;

        public SQLServerUserRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Save(User user)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string query = "INSERT INTO Users (Id, Email, Name, Password) VALUES (@Id, @Email, @Name, @Password)";

            using SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", user.Id);
            command.Parameters.AddWithValue("@Email", user.Email);
            command.Parameters.AddWithValue("@Name", user.Name);
            command.Parameters.AddWithValue("@Password", user.Password);

            command.ExecuteNonQuery();
        }

        public User? Search(string email)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string query = "SELECT Id, Email, Name, Password FROM Users WHERE Email = @Email";

            using SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Email", email);

            using SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new User
                {
                    Id = reader.GetString(0),
                    Email = reader.GetString(1),
                    Name = reader.GetString(2),
                    Password = reader.GetString(3)
                };
            }

            return null;
        }
    }
}
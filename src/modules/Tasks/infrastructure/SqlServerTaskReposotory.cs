using Tasks.Domain;
using Microsoft.Data.SqlClient;

namespace Tasks
{
    internal class SqlServerTasksRepository : TaskRepository
    {
        private readonly string connectionString;

        public SqlServerTasksRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<TodoTask> All()
        {
            var tasks = new List<TodoTask>();

            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string query = "SELECT Id, UserId, Title, Description, CreationDate, ExpirationDate, IsCompleted FROM Tasks";

            using SqlCommand command = new SqlCommand(query, connection);

            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                tasks.Add(new TodoTask(reader.GetString(1))
                {
                    Id = reader.GetString(0),
                    Title = reader.GetString(2),
                    Description = reader.GetString(3),
                    CreationDate = reader.GetDateTime(4),
                    ExpirationDate = reader.GetDateTime(5),
                    IsCompleted = reader.GetBoolean(6)
                });
            }

            return tasks;
        }

        public List<TodoTask> AllFromUser(string userId)
        {
            var tasks = new List<TodoTask>();

            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string query = "SELECT Id, UserId, Title, Description, CreationDate, ExpirationDate, IsCompleted FROM Tasks WHERE UserId = @UserId";

            using SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserId", userId);

            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                tasks.Add(new TodoTask(reader.GetString(1))
                {
                    Id = reader.GetString(0),
                    Title = reader.GetString(2),
                    Description = reader.GetString(3),
                    CreationDate = reader.GetDateTime(4),
                    ExpirationDate = reader.GetDateTime(5),
                    IsCompleted = reader.GetBoolean(6)
                });
            }

            return tasks;
        }

        public void Add(TodoTask task)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            string query = "INSERT INTO Tasks (Id, UserId, Title, Description, CreationDate, ExpirationDate, IsCompleted) VALUES (@Id, @UserId, @Title, @Description, @CreationDate, @ExpirationDate, @IsCompleted)";
            using SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", task.Id);
            command.Parameters.AddWithValue("@UserId", task.UserId);
            command.Parameters.AddWithValue("@Title", task.Title);
            command.Parameters.AddWithValue("@Description", task.Description);
            command.Parameters.AddWithValue("@CreationDate", task.CreationDate);
            command.Parameters.AddWithValue("@ExpirationDate", task.ExpirationDate);
            command.Parameters.AddWithValue("@IsCompleted", task.IsCompleted);

            command.ExecuteNonQuery();
        }


        public void Update(string id, UpdateTaskPayload payload)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string query = "UPDATE Tasks SET Title = @Title, Description = @Description, IsCompleted = @IsCompleted WHERE Id = @Id";

            using SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);
            command.Parameters.AddWithValue("@Title", payload.Title);
            command.Parameters.AddWithValue("@Description", payload.Description);
            command.Parameters.AddWithValue("@IsCompleted", payload.IsCompleted);

            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected == 0)
            {
                throw new InvalidOperationException("Task not found");
            }
        }

        public void Delete(string id)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string query = "DELETE FROM Tasks WHERE Id = @Id";

            using SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);

            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected == 0)
            {
                throw new InvalidOperationException("Task not found");
            }
        }

        public TodoTask? Search(string id)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string query = "SELECT Id, UserId, Title, Description, CreationDate, ExpirationDate, IsCompleted FROM Tasks WHERE Id = @Id";

            using SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);

            using SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new TodoTask(reader.GetString(1))
                {
                    Id = reader.GetString(0),
                    Title = reader.GetString(2),
                    Description = reader.GetString(3),
                    CreationDate = reader.GetDateTime(4),
                    ExpirationDate = reader.GetDateTime(5),
                    IsCompleted = reader.GetBoolean(6)
                };
            }

            return null;
        }
    }
}

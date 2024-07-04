using Microsoft.Data.SqlClient;

namespace Shared.Infrastructure
{
  public class Database
  {
    public static void Initialize()
    {
      string scriptFile = "mssql-init.sql";
      try
      {
        string script = File.ReadAllText(scriptFile);

        using SqlConnection connection = new SqlConnection(Env.Config.GetInitializationConnectionString());
        connection.Open();

        using SqlCommand command = new SqlCommand(script, connection);
        command.ExecuteNonQuery();
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error initializing database: {ex.Message}");
      }
    }
  }
}
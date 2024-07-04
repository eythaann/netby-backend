namespace Env
{
    public static class Config
    {
        public static readonly string ENVIRONMENT = Environment.GetEnvironmentVariable("ENV") ?? "local";
        public static readonly string SECRET_KEY = Environment.GetEnvironmentVariable("SECRET_KEY") ?? "HARD_CODED_SECRET_KEY_FOR_TESTING";
        public static readonly string PORT = Environment.GetEnvironmentVariable("PORT") ?? "3001";

        public static string GetConnectionString()
        {
            return Environment.GetEnvironmentVariable("CONNECTION_STRING") ?? "Server=host.docker.internal,1433;Database=master;User Id=SA;Password=netby-123;Encrypt=False;";
        }

        public static string GetInitializationConnectionString()
        {
            return Environment.GetEnvironmentVariable("CONNECTION_STRING") ?? "Server=host.docker.internal,1433;Database=master;User Id=SA;Password=netby-123;Encrypt=False;";
        }
    }
}
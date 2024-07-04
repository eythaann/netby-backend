namespace Env
{
    public static class Config
    {
        public static readonly string SECRET_KEY = Environment.GetEnvironmentVariable("SECRET_KEY") ?? "HARD_CODED_SECRET_KEY_FOR_TESTING";
        public static readonly string PORT = Environment.GetEnvironmentVariable("PORT") ?? "3001";
    }
}
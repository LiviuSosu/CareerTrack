using Microsoft.Extensions.Configuration;
namespace CareerTrack.Common
{
    public class Configuration : IConfiguration
    {
        const string path = @"C:\Users\lsosu\Work\Proiecte\Personale\CareerTrack\CareerTrack\CareerTrack.Common\appsettings.json";
        private readonly string loggingFilePath;
        public string LoggingFilePath { get => loggingFilePath; }
        private readonly string jwtSecretKey;
        public string JwtSecretKey { get => jwtSecretKey; }

        private readonly string jwtIssuer;
        public string JwtIssuer { get => jwtIssuer; }

        private readonly string jwtAudience;
        public string JwtAudience { get => jwtSecretKey; }

        private readonly string jwtLifeTime;
        public string JwtLifeTime { get => jwtLifeTime; }

        private readonly string displayUserErrorMessage;
        public string DisplayUserErrorMessage { get => displayUserErrorMessage; }

        private readonly string expectedRoleClaim;
        public string ExpectedRoleClaim { get => expectedRoleClaim; }

        public Configuration()
        {
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile(path, false);
            var root = configurationBuilder.Build();

            loggingFilePath = root.GetSection("Logging").GetSection("loggingFilePath").Value;
            jwtSecretKey = root.GetSection("JWT").GetSection("secret").Value;
            jwtIssuer = root.GetSection("JWT").GetSection("issuer").Value;
            jwtAudience = root.GetSection("JWT").GetSection("audience").Value;
            jwtLifeTime = root.GetSection("JWT").GetSection("lifeteme").Value;
            displayUserErrorMessage = root.GetSection("Errors").GetSection("DisplayUserErrorMessage").Value;
            expectedRoleClaim = root.GetSection("JWT").GetSection("expectedRoleClaim").Value;
        }
    }
}

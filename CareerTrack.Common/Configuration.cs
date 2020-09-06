using Microsoft.Extensions.Configuration;
using System.IO;

namespace CareerTrack.Common
{
    public class Configuration : IConfiguration
    {
        private readonly string loggingFilePath;
        public string LoggingFilePath { get => loggingFilePath; }

        private readonly string displayGenericUserErrorMessage;
        public string DisplayGenericUserErrorMessage { get => displayGenericUserErrorMessage; }

        private readonly string displayObjectNotFoundErrorMessage;
        public string DisplayObjectNotFoundErrorMessage { get => displayObjectNotFoundErrorMessage; }

        private readonly JWTConfiguration jwtConfiguration;
        public JWTConfiguration JWTConfiguration { get => jwtConfiguration; }

        public Configuration()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");

            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile(path, false);
            var root = configurationBuilder.Build();

            loggingFilePath = root.GetSection("Logging").GetSection("loggingFilePath").Value;

            var jwtSection = root.GetSection("JWT");
            jwtConfiguration = new JWTConfiguration(
                jwtSection.GetSection("secret").Value,
                jwtSection.GetSection("issuer").Value,
                jwtSection.GetSection("audience").Value,
                jwtSection.GetSection("lifeteme").Value,
                jwtSection.GetSection("expectedRoleClaim").Value
                );

            displayGenericUserErrorMessage = root.GetSection("Errors").GetSection("DisplayGenericUserErrorMessage").Value;
            displayObjectNotFoundErrorMessage = root.GetSection("Errors").GetSection("DisplayObjectNotFoundErrorMessage").Value;
        }
    }

    public class JWTConfiguration
    {
        public JWTConfiguration(string jwtSecretKey, string jwtIssuer, string jwtAudience, string jwtLifeTime, string expectedRoleClaim)
        {
            JwtSecretKey = jwtSecretKey;
            JwtIssuer = jwtIssuer;
            JwtAudience = jwtAudience;
            JwtLifeTime = jwtLifeTime;
            ExpectedRoleClaim = expectedRoleClaim;
        }

        public readonly string JwtSecretKey;

        public readonly string JwtIssuer;

        public readonly string JwtAudience;

        public readonly string JwtLifeTime;

        public readonly string ExpectedRoleClaim;
    }
}

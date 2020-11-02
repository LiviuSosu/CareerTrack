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

        private readonly string displayExistentUserExceptionMessage;
        public string DisplayExistentUserExceptionMessage { get => displayExistentUserExceptionMessage; }

        private readonly string displayPasswordsAreNotTheSameExceptionMessage;
        public string DisplayPasswordsAreNotTheSameExceptionMessage { get => displayPasswordsAreNotTheSameExceptionMessage; }
        private readonly JWTConfiguration jwtConfiguration;
        string noRolesAssignedExceptionMessage { get; }
        public string NoRolesAssignedExceptionMessage { get => noRolesAssignedExceptionMessage; }
        string userEmailNotConfirmedExceptionMessage { get; }
        public string UserEmailNotConfirmedExceptionMessage { get => userEmailNotConfirmedExceptionMessage; }
        public JWTConfiguration JWTConfiguration { get => jwtConfiguration; }

        private readonly EmailServiceConfiguration emailServiceConfiguration;
        public EmailServiceConfiguration EmailServiceConfiguration { get => emailServiceConfiguration; }
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
            displayExistentUserExceptionMessage = root.GetSection("Errors").GetSection("DisplayExistentUserExceptionMessage").Value;
            noRolesAssignedExceptionMessage = root.GetSection("Errors").GetSection("NoRolesAssignedExceptionMessage").Value;
            displayPasswordsAreNotTheSameExceptionMessage = root.GetSection("Errors").GetSection("DisplayPasswordsAreNotTheSameExceptionMessage").Value;
            userEmailNotConfirmedExceptionMessage = root.GetSection("Errors").GetSection("DisplayUserEmailNotConfirmedExceptionMessage").Value;

            var emailServiceConfigurationSection = root.GetSection("EmailServiceConfiguration");
            var emailAddressConfigurationSection = emailServiceConfigurationSection.GetSection("EmailAddress");
            var emailAddressConfiguration = new EmailAddress
            {
                Email = emailAddressConfigurationSection.GetSection("Email").Value,
                User = emailAddressConfigurationSection.GetSection("User").Value
            };

            emailServiceConfiguration = new EmailServiceConfiguration
            {
                EmailAddress = emailAddressConfiguration,
                Subject = emailServiceConfigurationSection.GetSection("Subject").Value,
                PlainTextContent = emailServiceConfigurationSection.GetSection("PlainTextContent").Value
            };
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

    public class EmailServiceConfiguration
    {
        private EmailAddress emailAddress;
        public EmailAddress EmailAddress { get => emailAddress; set { emailAddress = value; } }
        public string Subject { get; set; }
        public string PlainTextContent { get; set; }
    }

    public class EmailAddress
    {
        public string Email { get; set; }
        public string User { get; set; }
    }
}

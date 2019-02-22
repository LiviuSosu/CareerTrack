
namespace CareerTrack.Common
{
    public interface IConfiguration
    {
        string LoggingFilePath { get; }
        string JwtSecretKey { get; }
        string JwtLifeTime { get; }
        string JwtIssuer { get; }
        string JwtAudience { get; }
        string DisplayUserErrorMessage { get; }
        string ExpectedRoleClaim { get; }
    }
}

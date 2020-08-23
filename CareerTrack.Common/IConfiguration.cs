namespace CareerTrack.Common
{
    public interface IConfiguration
    {
        string DisplayGenericUserErrorMessage { get; }
        string DisplayObjectNotFoundErrorMessage { get; }

        string JwtSecretKey { get; }
        string JwtLifeTime { get; }
        string JwtIssuer { get; }
        string JwtAudience { get; }
        string ExpectedRoleClaim { get; }
    }
}

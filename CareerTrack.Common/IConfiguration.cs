namespace CareerTrack.Common
{
    public interface IConfiguration
    {
        string DisplayUserErrorMessage { get; }

        string JwtSecretKey { get; }
        string JwtLifeTime { get; }
        string JwtIssuer { get; }
        string JwtAudience { get; }
        string ExpectedRoleClaim { get; }
    }
}

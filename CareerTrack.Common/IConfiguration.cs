namespace CareerTrack.Common
{
    public interface IConfiguration
    {
        string DisplayGenericUserErrorMessage { get; }
        string DisplayObjectNotFoundErrorMessage { get; }

        JWTConfiguration JWTConfiguration { get; }
    }
}

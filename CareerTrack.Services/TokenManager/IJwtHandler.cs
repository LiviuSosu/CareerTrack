
namespace CareerTrack.Services.TokenManager
{
    public interface IJwtHandler
    {
        JsonWebToken Create(string username);
    }
}

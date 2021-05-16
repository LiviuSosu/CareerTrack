
namespace CareerTrack.Services.TokenManager
{
    public interface ITokenManager
    {
        void SetToken(string username, string jwtToken);
        void RevokeToken(string jwtToken);
    }
}

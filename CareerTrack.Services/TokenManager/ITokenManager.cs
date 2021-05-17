
using System.Threading.Tasks;

namespace CareerTrack.Services.TokenManager
{
    public interface ITokenManager
    {
        Task<bool> IsCurrentActiveToken();
        //void SetToken(string username, string jwtToken);
        //void RevokeToken(string jwtToken);
    }
}

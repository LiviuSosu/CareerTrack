using System.Threading.Tasks;

namespace CareerTrack.Services.TokenManager
{
    public interface ITokenManager
    {
        Task<bool> IsCurrentActiveToken();
        Task DeactivateCurrentAsync();
    }
}

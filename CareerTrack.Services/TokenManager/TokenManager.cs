using Microsoft.Extensions.Caching.Distributed;

namespace CareerTrack.Services.TokenManager
{
    public class TokenManager : ITokenManager
    {
        private readonly IDistributedCache _cache;

        public TokenManager(IDistributedCache cache)
        {
            _cache = cache;
        }

        public void SetToken(string username, string jwtToken)
        {
            _cache.SetString(username, jwtToken);
        }

        public void RevokeToken(string token)
        {
            _cache.Remove(token);
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Primitives;
using System.Linq;
using System.Threading.Tasks;

namespace CareerTrack.Services.TokenManager
{
    public class TokenManager : ITokenManager
    {
        private readonly IDistributedCache _cache;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TokenManager(IDistributedCache cache)
        {
            _cache = cache;
        }

        //public void SetToken(string username, string jwtToken)
        //{
        //    _cache.SetString(username, jwtToken);
        //}

        //public void RevokeToken(string token)
        //{
        //    _cache.Remove(token);
        //}

        public async Task<bool> IsCurrentActiveToken()
         => throw new System.NotImplementedException();//await IsActiveAsync(GetCurrentAsync());

        //private async Task<bool> IsActiveAsync(string token)
        //=> await _cache.GetStringAsync(GetKey(token)) == null;

        private string GetCurrentAsync()
        {
            var authorizationHeader = _httpContextAccessor
                .HttpContext.Request.Headers["authorization"];

            return authorizationHeader == StringValues.Empty
                ? string.Empty
                : authorizationHeader.Single().Split(" ").Last();
        }
    }
}

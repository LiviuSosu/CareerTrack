using CareerTrack.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CareerTrack.Services.TokenManager
{
    public class TokenManager : ITokenManager
    {
        private readonly IDistributedCache _cache;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly JWTConfiguration _jwtOptions;
        private readonly IConfiguration _configuration;
     //   private readonly ISet<RefreshToken> _refreshTokens = new HashSet<RefreshToken>();

        public TokenManager(IDistributedCache cache, IHttpContextAccessor httpContextAccessor,
            /*IOptions<JWTConfiguration> jwtOptions*/
            IConfiguration configuration)
        {
            _cache = cache;
            _httpContextAccessor = httpContextAccessor;


            //_jwtOptions = jwtOptions; //possible bug
            _configuration = configuration;
            _jwtOptions = configuration.JWTConfiguration;
        }

        public async Task<bool> IsCurrentActiveToken()
         => await IsActiveAsync(GetCurrentAsync());

        private async Task<bool> IsActiveAsync(string token)
        => await _cache.GetStringAsync(GetKey(token)) == null;

        private string GetCurrentAsync()
        {
            var authorizationHeader = _httpContextAccessor
                .HttpContext.Request.Headers["authorization"];

            return authorizationHeader == StringValues.Empty
                ? string.Empty
                : authorizationHeader.Single().Split(" ").Last();
        }

        private static string GetKey(string token)
        => $"tokens:{token}:deactivated";

        public async Task DeactivateCurrentAsync()
           => await DeactivateAsync(GetCurrentAsync());

        private async Task DeactivateAsync(string token)
            => await _cache.SetStringAsync(GetKey(token),
                " ", new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow =
                        TimeSpan.FromMinutes(5)
                });
    }
}

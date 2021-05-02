using CareerTrack.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using System;
using System.Linq;
using System.Threading.Tasks;

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
    }
}

using CareerTrack.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerTrack.Services.TokenManager
{
    public class JwtHandler : IJwtHandler
    {
        private readonly JWTConfiguration _jwtOptions;
        private readonly IConfiguration _configuration;

        public JwtHandler(IConfiguration configuration)
        {
            //_options = options.Value;
            //_securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
            //_signingCredentials = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha256);
            //_jwtHeader = new JwtHeader(_signingCredentials);

            _jwtOptions = configuration.JWTConfiguration;
        }

        public JsonWebToken Create(string username)
        {
            var nowUtc = DateTime.UtcNow;
            var expires = nowUtc.AddMinutes(_jwtOptions.ExpiryMinutes);
            var centuryBegin = new DateTime(1970, 1, 1).ToUniversalTime();
            var exp = (long)(new TimeSpan(expires.Ticks - centuryBegin.Ticks).TotalSeconds);
            var iat = (long)(new TimeSpan(nowUtc.Ticks - centuryBegin.Ticks).TotalSeconds);
            var payload = new JwtPayload
            {
                {"sub", username},
                {"iss", _jwtOptions.JwtIssuer},
                {"iat", iat},
                {"exp", exp},
                {"unique_name", username},
            };

            return null;
        }
    }
}

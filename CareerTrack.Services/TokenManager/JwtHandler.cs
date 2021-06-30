using CareerTrack.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Tokens;
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
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        private readonly SecurityKey _securityKey;
        private readonly IConfiguration _configuration;
        private readonly SigningCredentials _signingCredentials;
        private readonly JwtHeader _jwtHeader;
        private readonly ISet<RefreshToken> _refreshTokens = new HashSet<RefreshToken>();

        public JwtHandler(IConfiguration configuration)
        {
            //_options = options.Value;
            //_securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
            //_signingCredentials = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha256);
            //_jwtHeader = new JwtHeader(_signingCredentials);

            _jwtOptions = configuration.JWTConfiguration;
            _securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.JwtSecretKey));
            _signingCredentials = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha256);
            _jwtHeader = new JwtHeader(_signingCredentials);
        }

        public JsonWebToken Create(JwtHandlerDTO jwtHandlerDTO)
        {
            var nowUtc = DateTime.UtcNow;
            var expires = nowUtc.AddMinutes(_jwtOptions.ExpiryMinutes);
            var centuryBegin = new DateTime(1970, 1, 1).ToUniversalTime();
            var exp = (long)(new TimeSpan(expires.Ticks - centuryBegin.Ticks).TotalSeconds);
            var iat = (long)(new TimeSpan(nowUtc.Ticks - centuryBegin.Ticks).TotalSeconds);
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.JwtSecretKey));
            //var payload = new JwtPayload
            //{
            //    //{"sub", username},
            //    //{"iss", _jwtOptions.JwtIssuer},
            //    //{"iat", iat},
            //    //{"exp", exp},
            //    //{"unique_name", username},


            //    {"issuer", _jwtOptions.JwtIssuer},
            //    //{"audience", _jwtOptions.JwtAudience},
            //    {"iat", iat},
            //    {"exp", exp},
            //    {"claims", jwtHandlerDTO.Claims},
            //    {"signingCredentials", new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)},
            //    {"unique_name", jwtHandlerDTO.Username},
            //};



            var jwtToken = new JwtSecurityToken(
                   issuer: _jwtOptions.JwtIssuer,
                   audience: _jwtOptions.JwtAudience,
                   expires: DateTime.UtcNow.AddHours(Convert.ToInt16(_jwtOptions.JwtLifeTime)),
                   claims: jwtHandlerDTO.Claims,
                   signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
                   );

            var tokenValue = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            //if (GetExistingUserToken(responseToken) == null)
            //{
            //    _repoWrapper.UserToken.Create(responseToken);
            //}
            //else
            //{
            //    _repoWrapper.UserToken.Update(responseToken);
            //}

            //var jwt = new JwtSecurityToken(_jwtHeader, payload);
           // var token = _jwtSecurityTokenHandler.WriteToken(jwt);

            _refreshTokens.Add(new RefreshToken { Username = jwtHandlerDTO.Username });

            return new JsonWebToken
            {
                AccessToken = tokenValue,
                Expires = exp
            };
        }
    }
}

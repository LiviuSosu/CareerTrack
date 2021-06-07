using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerTrack.Services.TokenManager
{
    public class JwtHandler //: IJwtHandler
    {

        //public JwtHandler(IOptions<JwtOptions> options)
        //{
        //    _options = options.Value;
            //_securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
            //_signingCredentials = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha256);
            //_jwtHeader = new JwtHeader(_signingCredentials);
        //}
    }
}

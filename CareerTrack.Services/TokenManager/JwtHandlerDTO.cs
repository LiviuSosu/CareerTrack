using System.Collections.Generic;
using System.Security.Claims;

namespace CareerTrack.Services.TokenManager
{
    public class JwtHandlerDTO
    {
        public string Username { get; set; }
        public List<Claim> Claims { get; set; }
    }
}

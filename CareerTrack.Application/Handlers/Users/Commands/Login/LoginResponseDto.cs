using CareerTrack.Services.TokenManager;
using System;

namespace CareerTrack.Application.Handlers.Users.Commands.Login
{
    public class LoginResponseDTO
    {
        public string token { get; set; }
        public long expiration { get; set; }
        //   public JsonWebToken JsonWebToken { get; set; }
        public Guid UserId { get; set; }
        public string Username { get; set; }
    }
}

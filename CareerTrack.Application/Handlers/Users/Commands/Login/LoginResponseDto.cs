using System;

namespace CareerTrack.Application.Handlers.Users.Commands.Login
{
    public class LoginResponseDTO
    {
        public string token { get; set; }
        public DateTime expiration { get; set; }
    }
}

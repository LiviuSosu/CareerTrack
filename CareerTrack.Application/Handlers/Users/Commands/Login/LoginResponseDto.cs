using System;

namespace CareerTrack.Application.Handlers.Users.Commands.Login
{
    public class LoginResponseDto
    {
        public string token { get; set; }
        public DateTime expiration { get; set; }
    }
}

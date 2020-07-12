using CareerTrack.Domain.Entities;
using System.Text.Json.Serialization;

namespace CareerTrack.Models.Users
{
    public class LoginResponse
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string JwtToken { get; set; }
        [JsonIgnore] // refresh token is returned in http only cookie
        public string RefreshToken { get; set; }

        public LoginResponse(User user, string jwtToken, string refreshToken)
        {
            Id = user.Id;
            //FirstName = user.FirstName;
            //LastName = user.LastName;
            Username = user.Email;
            JwtToken = jwtToken;
            RefreshToken = refreshToken;
        }
    }
}

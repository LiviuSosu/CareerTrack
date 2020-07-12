using System.ComponentModel.DataAnnotations;

namespace CarerrTrack.Services.User
{
    public class AuthenticateRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}

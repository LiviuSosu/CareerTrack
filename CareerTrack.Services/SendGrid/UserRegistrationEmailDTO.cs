
namespace CareerTrack.Services.SendGrid
{
    public class UserRegistrationEmailDTO
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string ConfirmationToken { get; set; }
    }
}

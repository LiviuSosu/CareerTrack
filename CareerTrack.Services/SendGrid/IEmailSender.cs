using System.Threading.Tasks;

namespace CareerTrack.Services.SendGrid
{
    public interface IEmailSender
    {
        Task SendConfirmationEmail(UserRegistrationEmailDTO userRegistrationEmailDTO);
    }
}

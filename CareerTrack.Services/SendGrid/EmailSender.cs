using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace CareerTrack.Services.SendGrid
{
    public class EmailSender : IEmailSender
    {
        private readonly string apiKey;
        public EmailSender(string _apiKey)
        {
            apiKey = _apiKey;
        }

        public async Task SendConfirmationEmail(UserRegistrationEmailDTO userRegistrationEmailDTO)
        {
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(userRegistrationEmailDTO.EmailServiceConfiguration.EmailAddress.Email,
                userRegistrationEmailDTO.EmailServiceConfiguration.EmailAddress.User);

            var subject = string.Format(userRegistrationEmailDTO.EmailServiceConfiguration.Subject, userRegistrationEmailDTO.Username);
            var to = new EmailAddress(userRegistrationEmailDTO.Email, userRegistrationEmailDTO.Username);

            var plainTextContent = userRegistrationEmailDTO.EmailServiceConfiguration.PlainTextContent + userRegistrationEmailDTO.ConfirmationToken;

            var message = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, null);
            var response = await client.SendEmailAsync(message);
        }
    }
}

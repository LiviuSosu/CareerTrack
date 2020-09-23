using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Text;
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
            var from = new EmailAddress("test@example.com", "Example User");
        }
    }
}

using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerTrack.Application.Services.Mail
{
    public class EmailSender
    {
        public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }

        public AuthMessageSenderOptions Options { get; } //set only via Secret Manager


    }
}

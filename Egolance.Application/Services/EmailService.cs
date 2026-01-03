using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Egolance.Application.Services
{
    public class EmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            // For now, just log or simulate sending
            Console.WriteLine($"Sending email to {to}: {subject}");

            // Later: integrate SendGrid, MailKit, or SMTP
            await Task.CompletedTask;
        }
    }

}

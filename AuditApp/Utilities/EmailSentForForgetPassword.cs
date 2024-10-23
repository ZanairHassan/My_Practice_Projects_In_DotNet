using Microsoft.Graph;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public static class EmailSentForForgetPassword
    {
        public static async void SendEmailForPassword(string email, string password)
        {
            var apiKey = "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"; // Replace with your SendGrid API key
            var client = new SendGridClient(apiKey);

            var subject = "Password Reset Instructions";
            var message = $"Your temporary password is: {password}";

            var from = new EmailAddress("umairqamar344@gmail.com", "ASKAII APP");
            var to = new EmailAddress(email);

            var msg = MailHelper.CreateSingleEmail(from, to, subject, message, null);

            var response = await client.SendEmailAsync(msg);
        }
    }
}

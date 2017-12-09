using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace Portal.CMS.Web.Architecture.Helpers
{
    public static class EmailHelper
    {
        const string WEBSITE_NAME = "Website Name";
        const string EMAIL_FROM_ADDRESS = "Email From Address";
        const string SENDGRID_API_KEY = "SendGrid ApiKey";

        public static async Task SendEmailAsync(string recipientEmailAddress, string subject, string messageBody)
        {
            var apiKey = SettingHelper.Get(SENDGRID_API_KEY);
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(SettingHelper.Get(EMAIL_FROM_ADDRESS), SettingHelper.Get(WEBSITE_NAME));
            var to = new EmailAddress(recipientEmailAddress);
            var plainTextContent = messageBody;
            var htmlContent = messageBody;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            await client.SendEmailAsync(msg);
        }
    }
}
using SendGrid;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

namespace Portal.CMS.Web.Architecture.Helpers
{
    public static class EmailHelper
    {
        const string WEBSITE_NAME = "Website Name";
        const string EMAIL_FROM_ADDRESS = "Email From Address";
        const string SENDGRID_USERNAME = "SendGrid UserName";
        const string SENDGRID_PASSWORD = "SendGrid Password";

        public static void Send(List<string> recipients, string subject, string messageBody)
        {
            var myMessage = new SendGridMessage();

            var fromAddress = (SettingHelper.Get(EMAIL_FROM_ADDRESS));

            if (string.IsNullOrEmpty(fromAddress))
                return;

            myMessage.From = new MailAddress(fromAddress);

            myMessage.AddTo(recipients);

            myMessage.Subject = $"{SettingHelper.Get(WEBSITE_NAME)}: {subject}";
            myMessage.Html = messageBody;

            myMessage.EnableClickTracking();

            var username = SettingHelper.Get(SENDGRID_USERNAME);
            var password = SettingHelper.Get(SENDGRID_PASSWORD);

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return;

            var credentials = new NetworkCredential(username, password);

            var transportWeb = new SendGrid.Web(credentials);
            transportWeb.DeliverAsync(myMessage);
        }
    }
}
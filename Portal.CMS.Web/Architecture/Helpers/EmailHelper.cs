using SendGrid;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

namespace Portal.CMS.Web.Architecture.Helpers
{
    public static class EmailHelper
    {
        public static void Send(List<string> recipients, string subject, string messageBody)
        {
            var myMessage = new SendGridMessage();

            var fromAddress = (SettingHelper.Get("Email From Address"));

            if (string.IsNullOrEmpty(fromAddress))
                return;

            myMessage.From = new MailAddress(fromAddress);

            myMessage.AddTo(recipients);

            myMessage.Subject = string.Format("{0}: {1}", SettingHelper.Get("Website Name"), subject);
            myMessage.Html = messageBody;

            myMessage.EnableClickTracking();

            var username = SettingHelper.Get("SendGrid UserName");
            var password = SettingHelper.Get("SendGrid Password");

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return;

            var credentials = new NetworkCredential(username, password);

            var transportWeb = new SendGrid.Web(credentials);
            transportWeb.DeliverAsync(myMessage);
        }
    }
}
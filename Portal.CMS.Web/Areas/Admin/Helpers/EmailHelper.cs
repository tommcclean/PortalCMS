using SendGrid;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

namespace Portal.CMS.Web.Areas.Admin.Helpers
{
    public static class EmailHelper
    {
        public static void Send(List<string> recipients, string subject, string messageBody)
        {
            var myMessage = new SendGridMessage();

            myMessage.From = new MailAddress(SettingHelper.Get("Email From Address"));

            myMessage.AddTo(recipients);

            myMessage.Subject = subject;
            myMessage.Html = messageBody;

            myMessage.EnableClickTracking();

            var username = SettingHelper.Get("SendGrid UserName");
            var password = SettingHelper.Get("SendGrid Password");
            var credentials = new NetworkCredential(username, password);

            var transportWeb = new SendGrid.Web(credentials);
            transportWeb.DeliverAsync(myMessage);
        }
    }
}
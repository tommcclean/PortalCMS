using Microsoft.AspNet.Identity.Owin;
using PortalCMS.Entities.Entities.Models;
using PortalCMS.Library.ExtensionMethods;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PortalCMS.Web.Architecture.Helpers
{
	public class EmailHelper
	{
		private const string LOGO_IMAGE_URL = "/images/logo.png";
		private const string EMAIL_TEMPLATE_DIRECTORY = "/Content/EmailTemplates";

		const string WEBSITE_NAME = "Website Name";
		const string EMAIL_FROM_ADDRESS = "Email From Address";
		const string SENDGRID_API_KEY = "SendGrid ApiKey";

		public static bool IsEmailEnabled
		{
			get
			{
				return SettingHelper.Get(SENDGRID_API_KEY).HasValue();
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="user"></param>
		/// <param name="code"></param>
		/// <returns></returns>
		public static async Task SendAccountActivationLinkAsync(ApplicationUser user, string code, string callbackUrl)
		{
			// Send an email with this link
			var messageBody = LoadTemplate("AccountActivationLink", "Account Activation");
			messageBody = messageBody.Replace("@Model.ActionLink", callbackUrl);
			messageBody = messageBody.Replace("@Model.Name", user.FullName);

			await SendEmailAsync(user.Email, "Account activation link enclosed", messageBody);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="email"></param>
		/// <param name="token"></param>
		/// <returns></returns>
		public static async Task SendPasswordResetLinkAsync(ApplicationUser user, string token, string callbackUrl)
		{
			// Send an email with this link
			var messageBody = LoadTemplate("PasswordResetLink", "Password Reset");
			messageBody = messageBody.Replace("@Model.ActionLink", callbackUrl);
			messageBody = messageBody.Replace("@Model.Name", user.FullName);

			await SendEmailAsync(user.Email, "Password reset link enclosed", messageBody);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="email"></param>
		/// <returns></returns>
		public static async Task SendPasswordChangedAsync(string email, string fullName)
		{
			// Send an email
			var messageBody = LoadTemplate("PasswordChanged", "Account Notification");
			messageBody = messageBody.Replace("@Model.FullName", string.Format("{0:dd-MMM-yyyy}", DateTime.Now));
			messageBody = messageBody.Replace("@Model.ChangeDate", string.Format("{0:dd-MMM-yyyy}", DateTime.Now));

			await SendEmailAsync(email, "Password reset link enclosed", messageBody);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="email"></param>
		/// <param name="token"></param>
		/// <returns></returns>
		public static async Task SendContactUsMessageAsync(string toEmail, string fromEmail, string name, string subject, string message)
		{
			// Send an email
			var messageBody = LoadTemplate("ContactUsMessage", "Contact-Us Message Received");
			messageBody = messageBody.Replace("@Model.FromEmailAddress", fromEmail);
			messageBody = messageBody.Replace("@Model.Name", name);
			messageBody = messageBody.Replace("@Model.Subject", subject);
			messageBody = messageBody.Replace("@Model.Message", message);
			await SendEmailAsync(toEmail, "Password reset link enclosed", messageBody);
		}

		#region private methods

		private static async Task SendEmailAsync(string recipientEmailAddress, string subject, string messageBody)
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

		private static string LoadTemplate(string templateName, string templateTitle)
		{
			string emailTemplateBasePage = string.Format("{0}/Base/Base.html", EMAIL_TEMPLATE_DIRECTORY);
			string baseTemplate = File.OpenText(HttpContext.Current.Server.MapPath(string.Format("~/{0}", emailTemplateBasePage))).ReadToEnd();
			string body = File.OpenText(HttpContext.Current.Server.MapPath(string.Format("~/{0}/{1}.html", EMAIL_TEMPLATE_DIRECTORY, templateName))).ReadToEnd();
			string logoUrl = HttpContext.Current.Server.MapPath("/images/logo.png");
			string portalName = SettingHelper.Get(WEBSITE_NAME);
			string portalAddress = string.Empty;

			baseTemplate = baseTemplate.Replace("@Model.TemplateBody", body);
			baseTemplate = baseTemplate.Replace("@Model.Title", templateTitle);
			baseTemplate = baseTemplate.Replace("@Model.Logo", logoUrl);
			baseTemplate = baseTemplate.Replace("@Model.PortalName", portalName);
			baseTemplate = baseTemplate.Replace("@Model.PortalAddress", portalAddress);

			return baseTemplate;
		}

		#endregion
	}
}

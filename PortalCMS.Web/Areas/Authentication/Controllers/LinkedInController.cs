using Microsoft.Owin;
using RestSharp;
using System.Configuration;
using System.Web.Mvc;

namespace PortalCMS.Web.Areas.Authentication.Controllers
{
	public class LinkedInController : Controller
	{
		// GET: LinkedIn
		public ActionResult Index(string code, string state)
		{
			string clientId = ConfigurationManager.AppSettings["Linkedin.ClientID"];
			string clientSecret = ConfigurationManager.AppSettings["Linkedin.SecretKey"];

			//This method path is your return URL  
			try
			{
				//Get Accedd Token  
				var client = new RestClient("https://www.linkedin.com/oauth/v2/accessToken");
				var request = new RestRequest(Method.POST);
				request.AddParameter("grant_type", "authorization_code");
				request.AddParameter("code", code);
				request.AddParameter("redirect_uri", new PathString("/Index"));
				request.AddParameter("client_id", clientId);
				request.AddParameter("client_secret", clientSecret);
				IRestResponse response = client.Execute(request);
				var content = response.Content;
			}
			catch 
			{
				throw;
			}
			return View();
		}
	}
}
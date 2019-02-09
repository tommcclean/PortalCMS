using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Portal.CMS.Web.Startup))]
namespace Portal.CMS.Web
{
	public partial class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			ConfigureAuth(app);
		}
	}
}

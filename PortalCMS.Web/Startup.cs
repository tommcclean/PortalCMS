using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(PortalCMS.Web.Startup))]
namespace PortalCMS.Web
{
	public partial class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			ConfigureAuth(app);
		}
	}
}

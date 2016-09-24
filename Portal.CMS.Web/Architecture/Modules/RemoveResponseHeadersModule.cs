using System.Web;

namespace Portal.CMS.Web.Architecture.Modules
{
    public class RemoveResponseHeadersModule : IHttpModule
    {
        public void Init(HttpApplication application)
        {
            application.PreSendRequestHeaders += (sender, args) => HttpContext.Current.Response.Headers.Remove("Server");
        }

        public void Dispose()
        {
        }
    }
}
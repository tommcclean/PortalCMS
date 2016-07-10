using Portal.CMS.Services.PageBuilder;
using Portal.CMS.Web.DependencyResolution;
using StructureMap;
using System.Web.Mvc;
using System.Web.Routing;

namespace Portal.CMS.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            IContainer container = IoC.Initialize();

            PageService pageService = container.GetInstance<PageService>();

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            var pageList = pageService.Get();

            foreach (var page in pageList)
            {
                string targetRoute = string.Empty;

                if (string.IsNullOrWhiteSpace(page.PageArea) && page.PageController.Equals("Home", System.StringComparison.OrdinalIgnoreCase) && page.PageAction.Equals("Index", System.StringComparison.OrdinalIgnoreCase))
                {
                    routes.MapRoute(
                        string.Format("PageBuilder_{0}_Base", page.PageId),
                        "",
                        new { area = "Builder", controller = "Build", action = "Index", pageId = page.PageId }
                    );
                }

                if (page.PageAction.Equals("Index", System.StringComparison.OrdinalIgnoreCase))
                {
                    targetRoute = string.Format("{0}", page.PageController);

                    if (!string.IsNullOrWhiteSpace(page.PageArea))
                        targetRoute = string.Format("{0}/{1}", page.PageArea, page.PageController);

                    routes.MapRoute(
                        string.Format("PageBuilder_{0}_Index", page.PageId),
                        targetRoute,
                        new { area = "Builder", controller = "Build", action = "Index", pageId = page.PageId }
                    );
                }

                targetRoute = string.Format("{0}/{1}", page.PageController, page.PageAction);

                if (!string.IsNullOrWhiteSpace(page.PageArea))
                    targetRoute = string.Format("{0}/{1}/{2}", page.PageArea, page.PageController, page.PageAction);

                routes.MapRoute(
                    string.Format("PageBuilder_{0}", page.PageId),
                    targetRoute,
                    new { area = "Builder", controller = "Build", action = "Index", pageId = page.PageId }
                );
            }

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
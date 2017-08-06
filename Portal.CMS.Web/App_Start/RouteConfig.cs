using Portal.CMS.Services.PageBuilder;
using Portal.CMS.Web.Architecture.Helpers;
using Portal.CMS.Web.DependencyResolution;
using System.Web.Mvc;
using System.Web.Routing;

namespace Portal.CMS.Web
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            // INITIALISE: StructureMap Dependency Injection
            var container = IoC.Initialize();
            var pageService = container.GetInstance<PageService>();

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            var pageList = AsyncHelpers.RunSync(() => pageService.GetAsync());

            foreach (var page in pageList)
            {
                var targetRoute = string.Empty;

                if (string.IsNullOrWhiteSpace(page.PageArea) && page.PageController.Equals("Home", System.StringComparison.OrdinalIgnoreCase) && page.PageAction.Equals("Index", System.StringComparison.OrdinalIgnoreCase))
                    routes.MapRoute($"PageBuilder_{page.PageId}_Base", "", new { area = "PageBuilder", controller = "Page", action = "Index", pageId = page.PageId });

                if (page.PageAction.Equals("Index", System.StringComparison.OrdinalIgnoreCase))
                {
                    targetRoute = page.PageController;

                    if (!string.IsNullOrWhiteSpace(page.PageArea))
                        targetRoute = $"{page.PageArea}/{page.PageController}";

                    routes.MapRoute($"PageBuilder_{page.PageId}_Index", targetRoute, new { area = "PageBuilder", controller = "Page", action = "Index", pageId = page.PageId });
                }

                targetRoute = $"{page.PageController}/{page.PageAction}";

                if (!string.IsNullOrWhiteSpace(page.PageArea))
                    targetRoute = $"{page.PageArea}/{page.PageController}/{page.PageAction}";

                routes.MapRoute($"PageBuilder_{page.PageId}", targetRoute, new { area = "PageBuilder", controller = "Page", action = "Index", pageId = page.PageId });
            }

            routes.MapRoute(name: "Default", url: "{controller}/{action}/{id}", defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }
    }
}
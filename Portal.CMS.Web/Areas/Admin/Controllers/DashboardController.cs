using Portal.CMS.Services.Generic;
using Portal.CMS.Services.Posts;
using Portal.CMS.Web.Architecture.ActionFilters;
using Portal.CMS.Web.Areas.Admin.ViewModels.Dashboard;
using System.Linq;
using System.Web.Mvc;

namespace Portal.CMS.Web.Areas.Admin.Controllers
{
    [AdminFilter]
    public class DashboardController : Controller
    {
        #region Dependencies

        readonly IPostService _postService;
        readonly IImageService _imageService;

        public DashboardController(IPostService postService, IImageService imageService)
        {
            _postService = postService;
            _imageService = imageService;
        }

        #endregion Dependencies

        public ActionResult Index()
        {
            var model = new DashboardViewModel
            {
                LatestPost = _postService.GetLatest(),
                Media = _imageService.Get().ToList()
            };

            return View(model);
        }

        [AdminFilter]
        public ActionResult QuickAccess(QuickAccessPageType pageType, int contentId = 0)
        {
            var model = new QuickAccessViewModel
            {
                PageType = pageType
            };

            switch (pageType)
            {
                case QuickAccessPageType.ExitAdministrationPanel_Admin:
                    model.Categories.Add(ExitButton());
                    break;

                case QuickAccessPageType.AdministrationPanel_Admin:
                    model.Categories.Add(ExitButton());
                    break;

                case QuickAccessPageType.PageBuilder:
                    model.Categories.Add(new QuickAccessCategory { Icon = "fa fa-plus", DesktopText = "Add", MobileText = "Add", CssClass = "add", Actions = new System.Collections.Generic.List<QuickAccessAction> { new QuickAccessAction { Icon = "fa fa-plus", Text = "Add Section", JavaScript = "TogglePanel('section-panel');" }, new QuickAccessAction { Icon = "fa fa-plus", Text = "Add Component", JavaScript = "TogglePanel('component-panel');" }, new QuickAccessAction { Icon = "fa fa-plus", Text = "Add Blank Page", Link = Url.Action("Create", "PageManager", new { area = nameof(Admin) }), LaunchModal = true }, new QuickAccessAction { Icon = "fa fa-image", Text = "Upload Image", Link = Url.Action("UploadImage", "MediaManager", new { area = nameof(Admin) }), LaunchModal = true }, new QuickAccessAction { Icon = "fa fa-rss", Text = "Write Blog Post", Link = Url.Action("Create", "BlogManager", new { area = nameof(Admin) }), LaunchModal = true }, } });
                    model.Categories.Add(new QuickAccessCategory { Icon = "fa fa-cog", DesktopText = "Page Options", MobileText = "Options", CssClass = "options", Actions = new System.Collections.Generic.List<QuickAccessAction> { new QuickAccessAction { Icon = "fa fa-pencil", Text = "Edit Page", Link = Url.Action("Edit", "PageManager", new { area = nameof(Admin), pageId = contentId }), LaunchModal = true }, new QuickAccessAction { Icon = "fa fa-sort", Text = "Edit Order", JavaScript = "ChangeOrder()" }, } });
                    model.Categories.Add(MoreContent());
                    model.Categories.Add(AdminButton());
                    break;

                case QuickAccessPageType.BlogManager:
                    model.Categories.Add(new QuickAccessCategory { Icon = "fa fa-plus", DesktopText = "Add", MobileText = "Add", CssClass = "add", Actions = new System.Collections.Generic.List<QuickAccessAction> { new QuickAccessAction { Icon = "fa fa-rss", Text = "Write Blog Post", Link = Url.Action("Create", "BlogManager", new { area = nameof(Admin) }), LaunchModal = true }, new QuickAccessAction { Icon = "fa fa-image", Text = "Upload Image", Link = Url.Action("UploadImage", "MediaManager", new { area = nameof(Admin) }), LaunchModal = true }, } });
                    model.Categories.Add(new QuickAccessCategory { Icon = "fa fa-cog", DesktopText = "Post Options", MobileText = "Options", CssClass = "options", Actions = new System.Collections.Generic.List<QuickAccessAction> { new QuickAccessAction { Icon = "fa fa-pencil", Text = "Edit Post", Link = Url.Action("Edit", "BlogManager", new { area = nameof(Admin), postId = contentId }), LaunchModal = true }, } });
                    model.Categories.Add(MoreContent());
                    model.Categories.Add(AdminButton());
                    break;

                case QuickAccessPageType.UserManagement_Admin:
                    model.Categories.Add(new QuickAccessCategory { Icon = "fa fa-plus", DesktopText = "Add User", MobileText = "Add User", CssClass = "act", LaunchModal = true, Link = Url.Action("Create", "UserManager", new { area = nameof(Admin) }) });
                    model.Categories.Add(ExitButton());
                    break;

                case QuickAccessPageType.ThemeManager_Admin:
                    model.Categories.Add(new QuickAccessCategory { Icon = "fa fa-plus", DesktopText = "Add Theme", MobileText = "Add Theme", CssClass = "act", LaunchModal = true, Link = Url.Action("Create", "ThemeManager", new { area = nameof(Admin) }) });
                    model.Categories.Add(ExitButton());
                    break;

                case QuickAccessPageType.PageManager_Admin:
                    model.Categories.Add(new QuickAccessCategory { Icon = "fa fa-plus", DesktopText = "Add Page", MobileText = "Add Page", CssClass = "act", LaunchModal = true, Link = Url.Action("Create", "PageManager", new { area = nameof(Admin) }) });
                    model.Categories.Add(ExitButton());
                    break;

                case QuickAccessPageType.CopyManager_Admin:
                    model.Categories.Add(new QuickAccessCategory { Icon = "fa fa-plus", DesktopText = "Add Copy", MobileText = "Add Copy", CssClass = "act", LaunchModal = true, Link = Url.Action("Create", "CopyManager", new { area = nameof(Admin) }) });
                    model.Categories.Add(ExitButton());
                    break;

                case QuickAccessPageType.BlogManager_Admin:
                    model.Categories.Add(new QuickAccessCategory { Icon = "fa fa-plus", DesktopText = "Add Blog Post", MobileText = "Add Post", CssClass = "act", LaunchModal = true, Link = Url.Action("Create", "BlogManager", new { area = nameof(Admin) }) });
                    model.Categories.Add(new QuickAccessCategory { Icon = "fa fa-bookmark", DesktopText = "Add Category", MobileText = "Add Category", CssClass = "act", LaunchModal = true, Link = Url.Action("Add", "PostCategories", new { area = nameof(Admin) }) });
                    model.Categories.Add(ExitButton());
                    break;

                case QuickAccessPageType.MediaManager_Admin:
                    model.Categories.Add(new QuickAccessCategory { Icon = "fa fa-trash", DesktopText = "Delete Image", MobileText = "Delete", CssClass = "act delete" });
                    model.Categories.Add(new QuickAccessCategory { Icon = "fa fa-plus", DesktopText = "Upload Image", MobileText = "Upload Image", CssClass = "act", LaunchModal = true, Link = Url.Action("UploadImage", "MediaManager", new { area = nameof(Admin) }) });
                    model.Categories.Add(new QuickAccessCategory { Icon = "fa fa-camera", DesktopText = "Upload Font", MobileText = "Upload Font", CssClass = "act", LaunchModal = true, Link = Url.Action("UploadFont", "MediaManager", new { area = nameof(Admin) }) });
                    model.Categories.Add(ExitButton());
                    break;
            }

            return PartialView("_QuickAccess", model);
        }

        private static QuickAccessCategory MoreContent()
        {
            return new QuickAccessCategory
            {
                Icon = "fa fa-suitcase",
                DesktopText = "More Content",
                MobileText = "More",
                CssClass = "more",
                Actions = new System.Collections.Generic.List<QuickAccessAction> {
                            new QuickAccessAction { Icon = "fa fa-list", Text = "Page Manager", JavaScript = "TogglePanel('pages-panel');" },
                            new QuickAccessAction { Icon = "fa fa-rss", Text = "Blog Manager", JavaScript = "TogglePanel('blog-manager-panel');" },
                            new QuickAccessAction { Icon = "fa fa-paint-brush", Text = "Theme Manager", JavaScript = "TogglePanel('theme-manager-panel');" },
                        }
            };
        }

        private QuickAccessCategory AdminButton()
        {
            return new QuickAccessCategory
            {
                Icon = "fa fa-cog",
                DesktopText = string.Empty,
                MobileText = string.Empty,
                CssClass = "admin",
                Link = Url.Action(nameof(Index), "Dashboard", new { area = nameof(Admin) })
            };
        }

        private QuickAccessCategory ExitButton()
        {
            return new QuickAccessCategory
            {
                Icon = "fa fa-external-link",
                DesktopText = string.Empty,
                MobileText = string.Empty,
                CssClass = "admin",
                Link = Url.Action(nameof(Index), "Home", new { area = "" })
            };
        }
    }
}
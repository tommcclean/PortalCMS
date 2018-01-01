using Portal.CMS.Web.Architecture.ActionFilters;
using Portal.CMS.Web.Architecture.Helpers;
using Portal.CMS.Web.Areas.Admin.ViewModels.Dashboard;
using System;
using System.Web.Mvc;

namespace Portal.CMS.Web.Areas.Admin.Controllers
{
    [EditorFilter(ActionFilterResponseType.Page)]
    public class QuickAccessController : Controller
    {
        public ActionResult Render(QuickAccessPageType pageType, int contentId = 0)
        {
            var model = new QuickAccessViewModel
            {
                PageType = pageType
            };

            if (UserHelper.IsAdmin)
            {
                ApplyAdminQuickAccess(model, pageType, contentId);
            }
            else if (UserHelper.IsEditor)
            {
                ApplyEditorQuickAccess(model, pageType, contentId);
            }

            return PartialView("_Render", model);
        }

        private void ApplyAdminQuickAccess(QuickAccessViewModel model, QuickAccessPageType pageType, int contentId = 0)
        {
            switch (pageType)
            {
                case QuickAccessPageType.ExitAdministrationPanel_Admin:
                    model.Categories.Add(ExitButton());
                    break;

                case QuickAccessPageType.AdministrationPanel_Admin:
                    model.Categories.Add(ExitButton());
                    break;

                case QuickAccessPageType.PageBuilder:
                    model.Categories.Add(new QuickAccessCategory { Icon = "fa fa-plus", DesktopText = "Add", MobileText = "Add", CssClass = "add", Actions = new System.Collections.Generic.List<QuickAccessAction> { new QuickAccessAction { Icon = "fa fa-plus", Text = "Add Section", JavaScript = "QuickAccess.TogglePanel('section-panel');" }, new QuickAccessAction { Icon = "fa fa-plus", Text = "Add Component", JavaScript = "QuickAccess.TogglePanel('component-panel');" }, new QuickAccessAction { Icon = "fa fa-plus", Text = "Add Blank Page", Link = Url.Action("Create", "PageManager", new { area = nameof(Admin) }), LaunchModal = true }, new QuickAccessAction { Icon = "fa fa-image", Text = "Upload Image", Link = Url.Action("UploadImage", "MediaManager", new { area = nameof(Admin) }), LaunchModal = true }, new QuickAccessAction { Icon = "fa fa-rss", Text = "Write Blog Post", Link = Url.Action("Create", "BlogManager", new { area = nameof(Admin) }), LaunchModal = true }, } });
                    model.Categories.Add(new QuickAccessCategory { Icon = "fa fa-cog", DesktopText = "Page Options", MobileText = "Options", CssClass = "options", Actions = new System.Collections.Generic.List<QuickAccessAction> { new QuickAccessAction { Icon = "fa fa-pencil", Text = "Edit Page", Link = Url.Action("Edit", "PageManager", new { area = nameof(Admin), pageId = contentId }), LaunchModal = true }, new QuickAccessAction { Icon = "fa fa-sort", Text = "Edit Order", JavaScript = "PageBuilder.Order.Edit()" }, } });
                    model.Categories.Add(Content(true, true));
                    model.Categories.Add(AdminButton());
                    break;

                case QuickAccessPageType.BlogManager:
                    model.Categories.Add(new QuickAccessCategory { Icon = "fa fa-plus", DesktopText = "Add", MobileText = "Add", CssClass = "add", Actions = new System.Collections.Generic.List<QuickAccessAction> { new QuickAccessAction { Icon = "fa fa-rss", Text = "Write Blog Post", Link = Url.Action("Create", "BlogManager", new { area = nameof(Admin) }), LaunchModal = true }, new QuickAccessAction { Icon = "fa fa-image", Text = "Upload Image", Link = Url.Action("UploadImage", "MediaManager", new { area = nameof(Admin) }), LaunchModal = true }, } });
                    model.Categories.Add(new QuickAccessCategory { Icon = "fa fa-cog", DesktopText = "Post Options", MobileText = "Options", CssClass = "options", Actions = new System.Collections.Generic.List<QuickAccessAction> { new QuickAccessAction { Icon = "fa fa-pencil", Text = "Edit Post", Link = Url.Action("Edit", "BlogManager", new { area = nameof(Admin), postId = contentId }), LaunchModal = true }, } });
                    model.Categories.Add(Content(true, true));
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
        }

        private void ApplyEditorQuickAccess(QuickAccessViewModel model, QuickAccessPageType pageType, int contentId = 0)
        {
            switch (pageType)
            {
                case QuickAccessPageType.PageBuilder:
                    model.Categories.Add(new QuickAccessCategory { Icon = "fa fa-plus", DesktopText = "Add", MobileText = "Add", CssClass = "add", Actions = new System.Collections.Generic.List<QuickAccessAction> { new QuickAccessAction { Icon = "fa fa-image", Text = "Upload Image", Link = Url.Action("UploadImage", "MediaManager", new { area = nameof(Admin) }), LaunchModal = true }, new QuickAccessAction { Icon = "fa fa-rss", Text = "Write Blog Post", Link = Url.Action("Create", "BlogManager", new { area = nameof(Admin) }), LaunchModal = true }, } });
                    model.Categories.Add(Content(false, true));
                    break;

                case QuickAccessPageType.BlogManager:
                    model.Categories.Add(new QuickAccessCategory { Icon = "fa fa-plus", DesktopText = "Add", MobileText = "Add", CssClass = "add", Actions = new System.Collections.Generic.List<QuickAccessAction> { new QuickAccessAction { Icon = "fa fa-rss", Text = "Write Blog Post", Link = Url.Action("Create", "BlogManager", new { area = nameof(Admin) }), LaunchModal = true }, new QuickAccessAction { Icon = "fa fa-image", Text = "Upload Image", Link = Url.Action("UploadImage", "MediaManager", new { area = nameof(Admin) }), LaunchModal = true }, } });
                    model.Categories.Add(new QuickAccessCategory { Icon = "fa fa-cog", DesktopText = "Post Options", MobileText = "Options", CssClass = "options", Actions = new System.Collections.Generic.List<QuickAccessAction> { new QuickAccessAction { Icon = "fa fa-pencil", Text = "Edit Post", Link = Url.Action("Edit", "BlogManager", new { area = nameof(Admin), postId = contentId }), LaunchModal = true }, } });
                    model.Categories.Add(Content(false, true));
                    break;

                default:
                    throw new ArgumentException("Relevant Page Type Not Supplied");
            }
        }

        private static QuickAccessCategory Content(bool isAdmin, bool isEditor)
        {
            var moreContent = new QuickAccessCategory
            {
                Icon = "fa fa-suitcase",
                DesktopText = "Content",
                MobileText = "Content",
                CssClass = "more",
                Actions = new System.Collections.Generic.List<QuickAccessAction>()
            };

            if (isAdmin)
            {
                moreContent.Actions.Add(new QuickAccessAction { Icon = "fa fa-list", Text = "Page Manager", JavaScript = "QuickAccess.TogglePanel('pages-panel');" });
                moreContent.Actions.Add(new QuickAccessAction { Icon = "fa fa-paint-brush", Text = "Theme Manager", JavaScript = "QuickAccess.TogglePanel('theme-manager-panel');" });
            }

            if (isEditor)
            {
                moreContent.Actions.Add(new QuickAccessAction { Icon = "fa fa-rss", Text = "Blog Manager", JavaScript = "QuickAccess.TogglePanel('blog-manager-panel');" });
            }

            return moreContent;
        }

        private QuickAccessCategory AdminButton()
        {
            return new QuickAccessCategory
            {
                Icon = "fa fa-cog",
                DesktopText = string.Empty,
                MobileText = string.Empty,
                CssClass = "admin",
                Link = Url.Action("Index", "Dashboard", new { area = nameof(Admin) })
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
                Link = Url.Action("Index", "Home", new { area = "" })
            };
        }
    }
}
using System.Collections.Generic;

namespace Portal.CMS.Web.Areas.Admin.ViewModels.Dashboard
{
    public class QuickAccessViewModel
    {
        public QuickAccessPageType PageType { get; set; }

        public List<QuickAccessCategory> Categories { get; set; } = new List<QuickAccessCategory>();
    }

    public class QuickAccessCategory
    {
        public string Icon { get; set; }

        public string DesktopText { get; set; }

        public string MobileText { get; set; }

        public string CssClass { get; set; }

        public string Link { get; set; }

        public bool LaunchModal { get; set; }

        public List<QuickAccessAction> Actions { get; set; } = new List<QuickAccessAction>();
    }

    public class QuickAccessAction
    {
        public string Icon { get; set; }

        public string Text { get; set; }

        public string Link { get; set; }

        public string JavaScript { get; set; }

        public bool LaunchModal { get; set; }
    }

    public enum QuickAccessPageType
    {
        PageBuilder = 1,
        BlogManager = 2,
        ExitAdministrationPanel_Admin = 3,
        AdministrationPanel_Admin = 4,
        UserManagement_Admin = 5,
        ThemeManager_Admin = 6,
        PageManager_Admin = 7,
        CopyManager_Admin = 8,
        BlogManager_Admin = 9,
        MediaManager_Admin = 10,
    }
}
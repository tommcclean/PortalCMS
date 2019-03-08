using PortalCMS.Entities.Entities.Models;
using System.Collections.Generic;

namespace PortalCMS.Web.Areas.Admin.ViewModels.UserManager
{
	public class UsersViewModel
    {
        public IEnumerable<ApplicationUser> Users { get; set; }
    }
}
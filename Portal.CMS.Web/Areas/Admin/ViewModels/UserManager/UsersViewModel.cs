using Portal.CMS.Entities.Entities.Models;
using System.Collections.Generic;

namespace Portal.CMS.Web.Areas.Admin.ViewModels.UserManager
{
	public class UsersViewModel
    {
        public IEnumerable<ApplicationUser> Users { get; set; }
    }
}
using Portal.CMS.Entities.Entities;
using System.Collections.Generic;

namespace Portal.CMS.Web.Areas.Admin.ViewModels.UserManager
{
    public class UsersViewModel
    {
        public IEnumerable<User> Users { get; set; }
    }
}
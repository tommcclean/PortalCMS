using Portal.CMS.Entities.Entities.Authentication;
using System.Linq;

namespace Portal.CMS.Entities.Seed
{
    public static class RoleSeed
    {
        public static void Seed(PortalEntityModel context)
        {
            if (!context.Roles.Any(x => x.RoleName == "Admin"))
            {
                context.Roles.Add(new Role { RoleName = "Admin" });
            }

            if (!context.Roles.Any(x => x.RoleName == "Authenticated"))
            {
                context.Roles.Add(new Role { RoleName = "Authenticated" });
            }
        }
    }
}
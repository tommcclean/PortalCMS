using Portal.CMS.Entities.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Portal.CMS.Entities.Seed
{
    public static class RoleSeed
    {
        public static void Seed(PortalEntityModel context)
        {
            var roleList = context.Roles.ToList();

            var newRoles = new List<Role>();

            if (!roleList.Any(x => x.RoleName == "Admin"))
                newRoles.Add(new Role { RoleName = "Admin", IsAssignable = true });

            if (!roleList.Any(x => x.RoleName == "Editor"))
                newRoles.Add(new Role { RoleName = "Editor", IsAssignable = true });

            if (!roleList.Any(x => x.RoleName == "Authenticated"))
                newRoles.Add(new Role { RoleName = "Authenticated", IsAssignable = false });

            if (!roleList.Any(x => x.RoleName == "Anonymous"))
                newRoles.Add(new Role { RoleName = "Anonymous", IsAssignable = false });

            if (newRoles.Any())
            {
                context.Roles.AddRange(newRoles);

                context.SaveChanges();
            }
        }
    }
}
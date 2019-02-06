using Portal.CMS.Entities.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Portal.CMS.Entities.Seed
{
    public static class RoleSeed
    {
        public static void Seed(PortalDbContext context)
        {
            var roleList = context.Roles.ToList();

            var newRoles = new List<Role>();

            if (!roleList.Any(x => x.Name == "Admin"))
                newRoles.Add(new Role { Name = "Admin", IsAssignable = true });

            if (!roleList.Any(x => x.Name == "Editor"))
                newRoles.Add(new Role { Name = "Editor", IsAssignable = true });

            if (!roleList.Any(x => x.Name == "Authenticated"))
                newRoles.Add(new Role { Name = "Authenticated", IsAssignable = false });

            if (!roleList.Any(x => x.Name == "Anonymous"))
                newRoles.Add(new Role { Name = "Anonymous", IsAssignable = false });

            if (newRoles.Any())
            {
                context.Roles.AddRange(newRoles);

                context.SaveChanges();
            }
        }
    }
}
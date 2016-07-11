using Portal.CMS.Entities.Entities.PageBuilder;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Portal.CMS.Entities.Seed
{
    public class CopySeed
    {
        public static void Seed(PortalEntityModel context)
        {
            if (!context.CopySections.Any())
            {
                context.CopySections.Add(new Entities.Copy.Copy() { CopyName = "Analytics Script", CopyBody = "<script type=\"text/javascript\"></script>", DateAdded = DateTime.Now, DateUpdated = DateTime.Now });
            }
        }
    }
}
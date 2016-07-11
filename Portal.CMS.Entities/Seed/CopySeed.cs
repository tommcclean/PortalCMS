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
                context.CopySections.Add(new Entities.Copy.Copy() { CopyName = "Description Meta-Tag", CopyBody = "<meta name=\"description\" content=\"Portal CMS is a fully featured content management system with a powerful integrated page builder.\">", DateAdded = DateTime.Now, DateUpdated = DateTime.Now });
                context.CopySections.Add(new Entities.Copy.Copy() { CopyName = "Analytics", CopyBody = "", DateAdded = DateTime.Now, DateUpdated = DateTime.Now });
            }
        }
    }
}
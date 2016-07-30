using Portal.CMS.Entities.Entities.Copy;
using Portal.CMS.Entities.Entities.Posts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Portal.CMS.Entities.Seed
{
    public static class CopySeed
    {
        public static void Seed(PortalEntityModel context)
        {
            var copyList = context.CopySections.ToList();
            var newCopyList = new List<Copy>();

            if (!copyList.Any(x => x.CopyName == "No Posts Message"))
                newCopyList.Add(new Copy { CopyName = "No Posts Message", CopyBody = "<h4>No More Posts</h4><p>There aren't any more Posts to show you yet, but we are writing more so check back soon.</p>", DateAdded = DateTime.Now, DateUpdated = DateTime.Now });

            if (!copyList.Any(x => x.CopyName == "No Comments Message"))
                newCopyList.Add(new Copy { CopyName = "No Comments Message", CopyBody = "<h4>No Comments Left Yet</h4><p>Why not add your own comments or thoughts to get the chat started?</p>", DateAdded = DateTime.Now, DateUpdated = DateTime.Now });

            if (!copyList.Any(x => x.CopyName == "No Gallery Message"))
                newCopyList.Add(new Copy { CopyName = "No Gallery Message", CopyBody = "<h4>No Gallery Images</h4><p>Sometimes we add relevant images to a photo gallery, but we don't have any for this post.</p>", DateAdded = DateTime.Now, DateUpdated = DateTime.Now });

            if (newCopyList.Any())
                context.CopySections.AddRange(newCopyList);
        }
    }
}
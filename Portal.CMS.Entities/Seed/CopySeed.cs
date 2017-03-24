using Portal.CMS.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Portal.CMS.Entities.Seed
{
    public static class CopySeed
    {
        public static void Seed(PortalEntityModel context)
        {
            var copyList = context.CopyItems.ToList();
            var newCopyList = new List<CopyItem>();

            if (!copyList.Any(x => x.CopyName == "No Posts Message"))
                newCopyList.Add(new CopyItem { CopyName = "No Posts Message", CopyBody = "<h4>No More Posts</h4><p>There aren't any more Posts to show you yet, but we are writing more so check back soon.</p>", DateAdded = DateTime.Now, DateUpdated = DateTime.Now });

            if (!copyList.Any(x => x.CopyName == "No Comments Message"))
                newCopyList.Add(new CopyItem { CopyName = "No Comments Message", CopyBody = "<h4>No Comments Left Yet</h4><p>Why not add your own comments or thoughts to get the chat started?</p>", DateAdded = DateTime.Now, DateUpdated = DateTime.Now });

            if (!copyList.Any(x => x.CopyName == "No Gallery Message"))
                newCopyList.Add(new CopyItem { CopyName = "No Gallery Message", CopyBody = "<h4>No Gallery Images</h4><p>Sometimes we add relevant images to a photo gallery, but we don't have any for this post.</p>", DateAdded = DateTime.Now, DateUpdated = DateTime.Now });

            if (!copyList.Any(x => x.CopyName == "Sign In Message"))
                newCopyList.Add(new CopyItem { CopyName = "Sign In Message", CopyBody = "<p>Please enter your sign in details below.</p>", DateAdded = DateTime.Now, DateUpdated = DateTime.Now });

            if (!copyList.Any(x => x.CopyName == "Update Account Message"))
                newCopyList.Add(new CopyItem { CopyName = "Update Account Message", CopyBody = "<p>If you change your email address, please remember to use this when logging in.</p>", DateAdded = DateTime.Now, DateUpdated = DateTime.Now });

            if (!copyList.Any(x => x.CopyName == "Advanced Partial Message"))
                newCopyList.Add(new CopyItem { CopyName = "Advanced Partial Message", CopyBody = "<h2>Recent Blog Posts</h2><p>Here is a sample of our most recent blog posts. Check back periodically as we are writing more...</p>", DateAdded = DateTime.Now, DateUpdated = DateTime.Now });

            if (newCopyList.Any())
                context.CopyItems.AddRange(newCopyList);
        }
    }
}
using Portal.CMS.Entities.Entities;
using Portal.CMS.Entities.Enumerators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Portal.CMS.Entities.Seed
{
    public static class PostSeed
    {
        public static void Seed(PortalEntityModel context)
        {
            if (!context.Posts.Any())
            {
                var image = context.Images.First(x => x.ImageCategory == ImageCategory.General);

                var firstPost = new Post
                {
                    PostTitle = "My First Post",
                    PostDescription = "This is just an example post to get you started, why not change it or create a new post?",
                    PostBody = "<p>Coming Soon...</p>",
                    IsPublished = true,
                    DateAdded = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    PostCategoryId = context.PostCategories.First().PostCategoryId,
                    PostImages = new List<PostImage>
                    {
                        new PostImage() { ImageId = image.ImageId, PostImageType = PostImageType.Banner }
                    }
                };

                context.Posts.Add(firstPost);
            }
        }
    }
}
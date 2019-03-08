using PortalCMS.Entities.Entities;
using PortalCMS.Entities.Enumerators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PortalCMS.Entities.Seed
{
	public static class PostSeed
	{
		public static void Seed(PortalDbContext context)
		{
			if (!context.Posts.Any())
			{
				var image = context.Images.First(x => x.ImageCategory == ImageCategory.General);

				var firstPost = new Post
				{
					PostTitle = "My First Post",
					PostDescription = "This is just an example post to get you started, why not change it or create a new post?",
					PostBody = "<p>Coming Soon...</p>",
					PostAuthorUserId = context.Users.FirstOrDefault().Id,
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
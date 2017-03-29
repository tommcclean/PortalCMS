using Portal.CMS.Entities.Entities;
using Portal.CMS.Entities.Enumerators;
using System.Linq;

namespace Portal.CMS.Web.Architecture.Extensions
{
    public static class PostExtensions
    {
        public const string DEFAULT_IMAGE_PATH = "/Areas/PageBuilder/Content/Images/Sample/Sample-1.jpg";

        public static string BannerImageUrl(this Post post)
        {
            if (post.PostImages.Any(x => x.PostImageType == PostImageType.Banner))
                return post.PostImages.First(x => x.PostImageType == PostImageType.Banner).Image.ImagePath;

            return DEFAULT_IMAGE_PATH;
        }
    }
}
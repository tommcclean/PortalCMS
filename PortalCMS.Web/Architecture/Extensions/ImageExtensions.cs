using PortalCMS.Entities.Entities;
using PortalCMS.Web.Architecture.Helpers;

namespace PortalCMS.Web.Architecture.Extensions
{
    public static class ImageExtensions
    {
        public static string CDNImagePath(this Image image)
        {
            var cdnAddress = SettingHelper.Get("CDN Address");

            if (string.IsNullOrWhiteSpace(cdnAddress))
            {
                return image.ImagePath;
            }

            return $"{cdnAddress}{image.ImagePath}";
        }
    }
}
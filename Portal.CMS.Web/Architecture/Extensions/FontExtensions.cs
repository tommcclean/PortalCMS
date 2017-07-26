using Portal.CMS.Entities.Entities;
using Portal.CMS.Web.Architecture.Helpers;

namespace Portal.CMS.Web.Architecture.Extensions
{
    public static class FontExtensions
    {
        public static string CDNFontPath(this Font font)
        {
            var cdnAddress = SettingHelper.Get("CDN Address");

            if (string.IsNullOrWhiteSpace(cdnAddress))
            {
                return font.FontPath;
            }

            return $"{cdnAddress}{font.FontPath}";
        }
    }
}
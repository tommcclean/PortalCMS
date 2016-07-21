using Portal.CMS.Entities.Entities.Generic;
using System.Collections.Generic;
using System.Linq;

namespace Portal.CMS.Entities.Seed
{
    public static class MediaSeed
    {
        public static void Seed(PortalEntityModel context)
        {
            if (!context.Images.Any())
            {
                var images = new List<Image>();

                images.Add(new Image { ImagePath = "/Areas/Builder/Content/Images/Sample/icon-1.png", ImageCategory = ImageCategory.Icon });
                images.Add(new Image { ImagePath = "/Areas/Builder/Content/Images/Sample/icon-2.png", ImageCategory = ImageCategory.Icon });
                images.Add(new Image { ImagePath = "/Areas/Builder/Content/Images/Sample/icon-3.png", ImageCategory = ImageCategory.Icon });
                images.Add(new Image { ImagePath = "/Areas/Builder/Content/Images/Sample/icon-4.png", ImageCategory = ImageCategory.Icon });
                images.Add(new Image { ImagePath = "/Areas/Builder/Content/Images/Sample/icon-5.png", ImageCategory = ImageCategory.Icon });
                images.Add(new Image { ImagePath = "/Areas/Builder/Content/Images/Sample/icon-6.png", ImageCategory = ImageCategory.Icon });

                images.Add(new Image { ImagePath = "/Areas/Builder/Content/Images/Sample/pattern-1.png", ImageCategory = ImageCategory.Texture });

                images.Add(new Image { ImagePath = "/Areas/Builder/Content/Images/Sample/sample-1.jpg", ImageCategory = ImageCategory.General });
                images.Add(new Image { ImagePath = "/Areas/Builder/Content/Images/Sample/sample-2.jpg", ImageCategory = ImageCategory.General });
                images.Add(new Image { ImagePath = "/Areas/Builder/Content/Images/Sample/sample-3.jpg", ImageCategory = ImageCategory.General });
                images.Add(new Image { ImagePath = "/Areas/Builder/Content/Images/Sample/sample-4.jpg", ImageCategory = ImageCategory.General });
                images.Add(new Image { ImagePath = "/Areas/Builder/Content/Images/Sample/sample-5.jpg", ImageCategory = ImageCategory.General });
                images.Add(new Image { ImagePath = "/Areas/Builder/Content/Images/Sample/sample-6.jpg", ImageCategory = ImageCategory.General });

                context.Images.AddRange(images);
            }
        }
    }
}
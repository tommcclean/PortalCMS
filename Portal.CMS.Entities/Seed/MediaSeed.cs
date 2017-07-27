using Portal.CMS.Entities.Entities;
using Portal.CMS.Entities.Enumerators;
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

                images.Add(new Image { ImagePath = "/Areas/PageBuilder/Content/Images/Sample/icon-1.png", ImageCategory = ImageCategory.Icon });
                images.Add(new Image { ImagePath = "/Areas/PageBuilder/Content/Images/Sample/icon-2.png", ImageCategory = ImageCategory.Icon });
                images.Add(new Image { ImagePath = "/Areas/PageBuilder/Content/Images/Sample/icon-3.png", ImageCategory = ImageCategory.Icon });
                images.Add(new Image { ImagePath = "/Areas/PageBuilder/Content/Images/Sample/icon-4.png", ImageCategory = ImageCategory.Icon });
                images.Add(new Image { ImagePath = "/Areas/PageBuilder/Content/Images/Sample/icon-5.png", ImageCategory = ImageCategory.Icon });
                images.Add(new Image { ImagePath = "/Areas/PageBuilder/Content/Images/Sample/icon-6.png", ImageCategory = ImageCategory.Icon });
                images.Add(new Image { ImagePath = "/Areas/PageBuilder/Content/Images/Sample/icon-7.png", ImageCategory = ImageCategory.Icon });
                images.Add(new Image { ImagePath = "/Areas/PageBuilder/Content/Images/Sample/icon-8.png", ImageCategory = ImageCategory.Icon });
                images.Add(new Image { ImagePath = "/Areas/PageBuilder/Content/Images/Sample/icon-9.png", ImageCategory = ImageCategory.Icon });
                images.Add(new Image { ImagePath = "/Areas/PageBuilder/Content/Images/Sample/icon-10.png", ImageCategory = ImageCategory.Icon });
                images.Add(new Image { ImagePath = "/Areas/PageBuilder/Content/Images/Sample/icon-11.png", ImageCategory = ImageCategory.Icon });
                images.Add(new Image { ImagePath = "/Areas/PageBuilder/Content/Images/Sample/icon-12.png", ImageCategory = ImageCategory.Icon });

                images.Add(new Image { ImagePath = "/Areas/PageBuilder/Content/Images/Sample/pattern-1.png", ImageCategory = ImageCategory.Texture });
                images.Add(new Image { ImagePath = "/Areas/PageBuilder/Content/Images/Sample/texture-linen.png", ImageCategory = ImageCategory.Texture });
                images.Add(new Image { ImagePath = "/Areas/PageBuilder/Content/Images/Sample/texture-notebook.png", ImageCategory = ImageCategory.Texture });
                images.Add(new Image { ImagePath = "/Areas/PageBuilder/Content/Images/Sample/texture-office.png", ImageCategory = ImageCategory.Texture });

                images.Add(new Image { ImagePath = "/Areas/PageBuilder/Content/Images/Sample/sample-1.jpg", ImageCategory = ImageCategory.General });
                images.Add(new Image { ImagePath = "/Areas/PageBuilder/Content/Images/Sample/sample-2.jpg", ImageCategory = ImageCategory.General });
                images.Add(new Image { ImagePath = "/Areas/PageBuilder/Content/Images/Sample/sample-3.jpg", ImageCategory = ImageCategory.General });
                images.Add(new Image { ImagePath = "/Areas/PageBuilder/Content/Images/Sample/sample-4.jpg", ImageCategory = ImageCategory.General });
                images.Add(new Image { ImagePath = "/Areas/PageBuilder/Content/Images/Sample/sample-5.jpg", ImageCategory = ImageCategory.General });
                images.Add(new Image { ImagePath = "/Areas/PageBuilder/Content/Images/Sample/sample-6.jpg", ImageCategory = ImageCategory.General });
                images.Add(new Image { ImagePath = "/Areas/PageBuilder/Content/Images/Sample/sample-7.jpg", ImageCategory = ImageCategory.General });
                images.Add(new Image { ImagePath = "/Areas/PageBuilder/Content/Images/Sample/sample-8.jpg", ImageCategory = ImageCategory.General });
                images.Add(new Image { ImagePath = "/Areas/PageBuilder/Content/Images/Sample/sample-9.png", ImageCategory = ImageCategory.General });

                context.Images.AddRange(images);
            }
        }
    }
}
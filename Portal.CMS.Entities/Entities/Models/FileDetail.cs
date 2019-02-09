using Portal.CMS.Entities.Enumerators;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Web;

namespace Portal.CMS.Entities.Entities.Models
{
	public class FileDetail
  {
    [Display(Name = "File")]
    public byte[] FileContent { get; set; }

    public int FileLength { get; set; }

    public ImageCategory ContentType { get; set; }

    public FileDetail() { }

    public FileDetail(HttpPostedFileBase fileUpload, ImageCategory contentType = ImageCategory.Profile)
    {
      if (fileUpload != null)
      {
        Stream fileStream = fileUpload.InputStream;
        int fileLength = fileUpload.ContentLength;

        this.FileLength = fileLength;
        this.ContentType = contentType;
        this.FileContent = new byte[fileLength];

        fileStream.Read(this.FileContent, 0, fileLength);
      }
    }

		public static byte[] LoadImageBytesFromPath(string directory, string fileName)
		{
			const string importDirectory = "~/App_Data";
			byte[] imageData = null;
			var imagePath = Path.Combine(HttpContext.Current.Server.MapPath(importDirectory), directory, fileName);

			if (File.Exists(imagePath))
			{
				imageData = File.ReadAllBytes(imagePath);
			}

			return imageData;
		}

		public static FileDetail LoadImageFromPath(string importDirectory="~/App_Data", string fileName="noImage.png", ImageCategory imageCategory= ImageCategory.General)
		{
			byte[] imageData = null;
			var imagePath = Path.Combine(HttpContext.Current.Server.MapPath(importDirectory), fileName);

			if (File.Exists(imagePath))
			{
				imageData = File.ReadAllBytes(imagePath);
			}

			return new FileDetail
			{
				FileLength = imageData.Length,
				ContentType = imageCategory,
				FileContent = imageData
			};
		}
	}
}

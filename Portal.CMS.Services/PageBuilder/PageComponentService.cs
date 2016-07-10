using Portal.CMS.Entities;
using Portal.CMS.Services.Shared;
using System.Linq;

namespace Portal.CMS.Services.PageBuilder
{
    public class PageComponentService
    {
        #region Dependencies

        private readonly PortalEntityModel _context = new PortalEntityModel();

        //public PageComponentService(PortalEntityModel context)
        //{
        //    _context = context;
        //}

        #endregion Dependencies

        public void Delete(int pageSectionId, string componentId)
        {
            var pageSection = _context.PageSections.FirstOrDefault(x => x.PageSectionId == pageSectionId);

            if (pageSection == null)
                return;

            pageSection.PageSectionBody = DocumentHelper.RemoveElementById(pageSection.PageSectionBody, componentId);

            _context.SaveChanges();
        }

        public void EditImage(int pageSectionId, string elementId, int selectedImageId)
        {
            var pageSection = _context.PageSections.FirstOrDefault(x => x.PageSectionId == pageSectionId);

            if (pageSection == null)
                return;

            var image = _context.Images.FirstOrDefault(x => x.ImageId == selectedImageId);

            if (image == null)
                return;

            pageSection.PageSectionBody = DocumentHelper.UpdateElementStyle(pageSection.PageSectionBody, elementId, image.ImagePath);

            _context.SaveChanges();
        }

        public void Anchor(int pageSectionId, string elementId, string elementText, string elementTarget, string elementColour)
        {
            var pageSection = _context.PageSections.FirstOrDefault(x => x.PageSectionId == pageSectionId);

            if (pageSection == null)
                return;

            var sectionBody = DocumentHelper.UpdateElementContent(pageSection.PageSectionBody, elementId, elementText);

            sectionBody = DocumentHelper.UpdateElementAttribute(sectionBody, elementId, "href", elementTarget);

            if (!string.IsNullOrWhiteSpace(elementColour))
                sectionBody = DocumentHelper.UpdateElementColour(sectionBody, elementId, elementColour);

            pageSection.PageSectionBody = sectionBody;

            _context.SaveChanges();
        }
    }
}
using System.ComponentModel;

namespace Portal.CMS.Web.Areas.Builder.ViewModels.Component
{
    public class ElementViewModel
    {
        [DisplayName("Value")]
        public string ElementValue { get; set; }

        [DisplayName("Colour")]
        public string ElementColour { get; set; }

        #region Hidden Properties

        public int PageId { get; set; }

        public int SectionId { get; set; }

        public string ElementId { get; set; }

        #endregion Hidden Properties
    }
}
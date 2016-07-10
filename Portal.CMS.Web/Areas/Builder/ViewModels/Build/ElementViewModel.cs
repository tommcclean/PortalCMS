using System.ComponentModel;

namespace Portal.CMS.Web.Areas.Builder.ViewModels.Build
{
    public class ElementViewModel
    {
        public int PageId { get; set; }

        public int SectionId { get; set; }

        public string ElementId { get; set; }

        [DisplayName("Value")]
        public string ElementValue { get; set; }

        [DisplayName("Colour")]
        public string ElementColour { get; set; }
    }
}
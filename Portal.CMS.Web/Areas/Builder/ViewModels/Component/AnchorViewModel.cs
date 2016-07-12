using System.ComponentModel;

namespace Portal.CMS.Web.Areas.Builder.ViewModels.Component
{
    public class AnchorViewModel
    {
        [DisplayName("Text")]
        public string ElementText { get; set; }

        [DisplayName("Target")]
        public string ElementTarget { get; set; }

        [DisplayName("Colour")]
        public string ElementColour { get; set; }

        #region Hidden Properties

        public int SectionId { get; set; }

        public string ElementId { get; set; }

        #endregion Hidden Properties
    }
}
using Portal.CMS.Entities.Entities.PageBuilder;
using System.Linq;

namespace Portal.CMS.Entities.Seed
{
    public class PageSectionTypeSeed
    {
        public static void Seed(PortalEntityModel context)
        {
            if (!context.PageSectionTypes.Any(x => x.PageSectionTypeName == "Introduction"))
            {
                context.PageSectionTypes.Add(new PageSectionType { PageSectionTypeName = "Introduction", PageSectionTypeBody = "<section id=\"section-<sectionId>\" class=\"header background-parallax height-tall\"><div class=\"overlay-medium\"></div><div id=\"component-<componentStamp>-<sectionId>\" class=\"vertical-alignment component-container\"><h1 id=\"title-<sectionId>\" data-section=\"1\">Custom Page</h1><p id=\"subtitle-<sectionId>\">This is copy that can be updated and changed.</p></div></section>" });
            }

            if (!context.PageSectionTypes.Any(x => x.PageSectionTypeName == "Empty Section"))
            {
                context.PageSectionTypes.Add(new PageSectionType { PageSectionTypeName = "Empty Section", PageSectionTypeBody = "<section id=\"section-<sectionId>\" class=\"blank height-medium background-static\"></section>" });
            }

            if (!context.PageSectionTypes.Any(x => x.PageSectionTypeName == "Block Text"))
            {
                context.PageSectionTypes.Add(new PageSectionType { PageSectionTypeName = "Block Text", PageSectionTypeBody = "<section id=\"section-<sectionId>\" class=\"block block-text height-tiny\"><h1 id=\"title-<sectionId>\">Call for a Quote</h1><h1 id=\"subtitle-<sectionId>\">00000 000 000</h1></section>" });
            }

            if (!context.PageSectionTypes.Any(x => x.PageSectionTypeName == "Quote"))
            {
                context.PageSectionTypes.Add(new PageSectionType { PageSectionTypeName = "Quote", PageSectionTypeBody = "<section id=\"section-<sectionId>\" class=\"quote\"><div class=\"quote-image-left\"></div><div class=\"quote-image-right\"></div><div id=\"container-<componentStamp>-<sectionId>\" class=\"vertical-alignment component-container\"><h1 id=\"quote-<componentStamp>-<sectionId>\">Perfection is not attainable, but if we chase perfection we can catch.</h1><p id=\"component-<componentStamp>-<sectionId>\">Tom McClean</p></div></section>" });
            }
        }
    }
}
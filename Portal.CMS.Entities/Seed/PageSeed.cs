using Portal.CMS.Entities.Entities.PageBuilder;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Portal.CMS.Entities.Seed
{
    public class PageSeed
    {
        public static void Seed(PortalEntityModel context)
        {
            if (!context.Pages.Any())
            {
                var homepage = new Entities.PageBuilder.Page()
                {
                    PageName = "Homepage",
                    PageController = "Home",
                    PageAction = "Index",
                    DateAdded = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    PageSections = new List<PageSection>() { new Entities.PageBuilder.PageSection() { PageSectionTypeId = context.PageSectionTypes.First(x => x.PageSectionTypeName == "Introduction").PageSectionTypeId, PageSectionBody = "<section id=\"section-1\" class=\"header background-parallax height-tall\"><div class=\"overlay-medium\"></div><div id=\"component-080716203305-1\" class=\"vertical-alignment component-container\"><h1 id=\"title-1\" data-section=\"1\">Portal CMS</h1><p id=\"subtitle-1\">Portal CMS is a fully featured content management system with an integrated Page Builder.</p></div></section>" } }
                };

                context.Pages.Add(homepage);

                context.Pages.Add(new Entities.PageBuilder.Page() { PageName = "Contact", PageController = "Contact", PageAction = "Index", DateAdded = DateTime.Now, DateUpdated = DateTime.Now });
            }
        }
    }
}
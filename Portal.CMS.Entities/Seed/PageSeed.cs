using Portal.CMS.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Portal.CMS.Entities.Seed
{
    public static class PageSeed
    {
        public static void Seed(PortalEntityModel context)
        {
            if (context.Pages.Any())
                return;

            context.Pages.Add(new Page
            {
                PageId = 1,
                PageName = "Home",
                PageController = "Home",
                PageAction = "Index",
                DateAdded = DateTime.Now,
                DateUpdated = DateTime.Now,
                PageAssociations = new List<PageAssociation>
                {
                    new PageAssociation { PageSection = new PageSection { PageSectionBody = "<section id=\"section-1\" class=\"header background-parallax height-tall\"><div class=\"overlay-medium\"></div><div id=\"component-250716214738-1\" class=\"vertical-alignment component-container\"><h1 id=\"component-250716214837-1\">Welcome to Portal CMS</h1><p id=\"component-250716214849-1\"><span style=\"line-height: 28.5714px;\">Incase you didn't already know, Portal CMS is a fully featured content management system with a powerful integrated Page Builder.</span></p><p id=\"component-250716214853-1\">Your new website is going to be great! But you need to do one thing before you start making your masterpiece.</p><a id=\"component-250716214932-1\" href=\"/Admin/Authentication/Register\" class=\"btn launch-modal\" data-title=\"Register Admin Account\">Register Admin Account</a></div></section>" },},
                    new PageAssociation { PageSection = new PageSection { PageSectionBody = "<section id=\"section-2\" class=\"blank height-medium background-static\" style=\"background-size: contain;\"><h1 id=\"title-250716215109-2\">What do you need to know?</h1><p id=\"subtitle-250716215109-2\">Portal CMS is designed to be easy to use, but if you are new to the technology here are a few useful pointers.</p><div id=\"component-250716215109-2\" class=\"container component-container\"><div id=\"row-250716215109-2\" class=\"row component-container\"><div id=\"group-1-250716215109-2\" class=\"col-xs-12 col-sm-6 component-container\"><div id=\"image-1-250716215109-2\" class=\"image image-standard\" style=\"background-image: url('/Areas/PageBuilder/Content/Images/Sample/sample-1.jpg');\"></div><h4 id=\"title-1-250716215109-2\">Click or Tap to Edit Content</h4><p id=\"subtitle-1-250716215109-2\">In the Page Builder you can change anything, simply click or tap it and change away. You don't need to worry about saving, we take care of that automatically. You can even add your own components and sections. Why not play around to learn how it works?</p></div><div id=\"group-2-250716215109-2\" class=\"col-xs-12 col-sm-6 component-container\"><div id=\"image-2-250716215109-2\" class=\"image image-standard\" style=\"background-image: url('/Areas/PageBuilder/Content/Images/Sample/sample-3.jpg');\"></div><h4 id=\"title-2-250716215109-2\">Visit the Administration Panel</h4><p id=\"subtitle-2-250716215109-2\">The Administration Panel contains everything you need to manage your website. You can manage your Media collection, write blog posts, create new pages and more. To access the Administration Panel simply login and click \"Administration\" in the top right.</p></div></div></div></section>" },},
                    new PageAssociation { PageSection = new PageSection { PageSectionBody = "<section id=\"section-3\" class=\"highlight height-standard background-parallax\" style=\"background-image: url('/Areas/PageBuilder/Content/Images/Sample/sample-2.jpg');background-size: cover;\"><div id=\"component-container-250716215403-3\" class=\"vertical-alignment component-container\"><h1 id=\"title-250716215403-3\" style=\"color: rgb(255, 255, 255);\">More Information</h1><p id=\"text-1-250716215403-3\" style=\"color: rgb(255, 255, 255);\">If you want to learn more about Portal CMS, you can take a look at our promotional website.</p><p id=\"text-2-250716215403-3\" style=\"color: rgb(255, 255, 255);\">We have created a series of demonstration videos, so if you want some more tips on how to use the technology, its&nbsp;worth a visit.</p><a id=\"component-250716215457-3\" href=\"http://www.portalcms.online\" class=\"btn\" target=\"_blank\">Learn More</a></div><div id=\"overlay-250716215403-3\" class=\"overlay overlay-medium\"></div></section>" },},
                    new PageAssociation { PageSection = new PageSection { PageSectionBody = "<section id=\"section-4\" class=\"blank height-small background-static\" style=\"background-size: contain;\"><h1 id=\"title-31072016183145-4\">Recent Posts</h1><p id=\"text-31072016183145-4\">Here are the most recent posts on our blog.</p><div id=\"widget-31072016183145-4\" class=\"widget-wrapper post-list-wrapper\" style=\"text-align: left;\"><div class=\"vertical-alignment\"><div class=\"loading-wrapper\" style=\"text-align: center;\"><div style=\"background-color: black; padding: 10px; display: inline-block;\"><img src=\"/Areas/PageBuilder/Content/Images/Sample/loading-graphic.gif\"></div></div></div></div></section>" },},
                }
            });
        }
    }
}
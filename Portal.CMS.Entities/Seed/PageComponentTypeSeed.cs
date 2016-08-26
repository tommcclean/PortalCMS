using Portal.CMS.Entities.Entities.PageBuilder;
using System.Collections.Generic;
using System.Linq;

namespace Portal.CMS.Entities.Seed
{
    public static class PageComponentTypeSeed
    {
        public static void Seed(PortalEntityModel context)
        {
            var componentList = context.PageComponentTypes.ToList();

            var newComponents = new List<PageComponentType>();

            if (!componentList.Any(x => x.PageComponentTypeName == "Heading (H1)"))
                newComponents.Add(new PageComponentType { PageComponentTypeName = "Heading (H1)", PageComponentTypeCategory = PageComponentTypeCategory.Text, PageComponentTypeDescription = "Large Title Text Element.", PageComponentBody = "<h1 id=\"component-<componentStamp>-<sectionId>\">Large Title</h1>" });

            if (!componentList.Any(x => x.PageComponentTypeName == "Heading (H2)"))
                newComponents.Add(new PageComponentType { PageComponentTypeName = "Heading (H2)", PageComponentTypeCategory = PageComponentTypeCategory.Text, PageComponentTypeDescription = "Large Title Text Element", PageComponentBody = "<h2 id=\"component-<componentStamp>-<sectionId>\">Medium Title</h2>" });

            if (!componentList.Any(x => x.PageComponentTypeName == "Heading (H3)"))
                newComponents.Add(new PageComponentType { PageComponentTypeName = "Heading (H3)", PageComponentTypeCategory = PageComponentTypeCategory.Text, PageComponentTypeDescription = "Title Text Element", PageComponentBody = "<h3 id=\"component-<componentStamp>-<sectionId>\">Small Title</h3>" });

            if (!componentList.Any(x => x.PageComponentTypeName == "Heading (H4)"))
                newComponents.Add(new PageComponentType { PageComponentTypeName = "Heading (H4)", PageComponentTypeCategory = PageComponentTypeCategory.Text, PageComponentTypeDescription = "Title Text Element", PageComponentBody = "<h4 id=\"component-<componentStamp>-<sectionId>\">Tiny Title</h4>" });

            if (!componentList.Any(x => x.PageComponentTypeName == "Paragraph"))
                newComponents.Add(new PageComponentType { PageComponentTypeName = "Paragraph", PageComponentTypeCategory = PageComponentTypeCategory.Text, PageComponentTypeDescription = "Single Paragraph Element.", PageComponentBody = "<p id=\"component-<componentStamp>-<sectionId>\">Standard Paragraph: Lorem ipsum dolor sit amet, eu qui accusam scriptorem. Purto erat facilisis sea id, vulputate deseruisse eos te, errem doctus feugiat te prim.</p>" });

            if (!componentList.Any(x => x.PageComponentTypeName == "Link"))
                newComponents.Add(new PageComponentType { PageComponentTypeName = "Link", PageComponentTypeCategory = PageComponentTypeCategory.Text, PageComponentTypeDescription = "Link to Another Page", PageComponentBody = "<a id=\"component-<componentStamp>-<sectionId>\" href=\"#\">Hyperlink: Click me to find out more.</a>" });

            if (!componentList.Any(x => x.PageComponentTypeName == "Button"))
                newComponents.Add(new PageComponentType { PageComponentTypeName = "Button", PageComponentTypeCategory = PageComponentTypeCategory.Text, PageComponentTypeDescription = "Button Link to Another Page", PageComponentBody = "<a id=\"component-<componentStamp>-<sectionId>\" href=\"#\" class=\"btn\">Rounded Button: Click Me</a>" });

            if (!componentList.Any(x => x.PageComponentTypeName == "Code"))
                newComponents.Add(new PageComponentType { PageComponentTypeName = "Code", PageComponentTypeCategory = PageComponentTypeCategory.Text, PageComponentTypeDescription = "Single Paragraph Element.", PageComponentBody = "<code id=\"component-<componentStamp>-<sectionId>\">Code Block: Lorem ipsum dolor sit amet, eu qui accusam scriptorem. Purto erat facilisis sea id, vulputate deseruisse eos te, errem doctus feugiat te prim.</code>" });

            if (!componentList.Any(x => x.PageComponentTypeName == "Freestyle"))
                newComponents.Add(new PageComponentType { PageComponentTypeName = "Freestyle", PageComponentTypeCategory = PageComponentTypeCategory.Text, PageComponentTypeDescription = "Single Paragraph Element.", PageComponentBody = "<div id=\"freestyle-<componentStamp>-<sectionId>\" class=\"freestyle\"><h1>Freestyle</h1><p>Anything goes in a Freestyle Component, the limits are your imagination.</p></div>" });

            if (!componentList.Any(x => x.PageComponentTypeName == "Avatar"))
                newComponents.Add(new PageComponentType { PageComponentTypeName = "Avatar", PageComponentTypeCategory = PageComponentTypeCategory.Image, PageComponentTypeDescription = "Small Circular Image", PageComponentBody = "<div id=\"component-<componentStamp>-<sectionId>\" class=\"image image-circle\"></div>" });

            if (!componentList.Any(x => x.PageComponentTypeName == "Square"))
                newComponents.Add(new PageComponentType { PageComponentTypeName = "Square", PageComponentTypeCategory = PageComponentTypeCategory.Image, PageComponentTypeDescription = "Small Square Image", PageComponentBody = "<div id=\"component-<componentStamp>-<sectionId>\" class=\"image image-square\"></div>" });

            if (!componentList.Any(x => x.PageComponentTypeName == "Icon"))
                newComponents.Add(new PageComponentType { PageComponentTypeName = "Icon", PageComponentTypeCategory = PageComponentTypeCategory.Image, PageComponentTypeDescription = "Tiny Square Image", PageComponentBody = "<div id=\"component-<componentStamp>-<sectionId>\" class=\"image image-icon\"></div>" });

            if (!componentList.Any(x => x.PageComponentTypeName == "Image"))
                newComponents.Add(new PageComponentType { PageComponentTypeName = "Image", PageComponentTypeCategory = PageComponentTypeCategory.Image, PageComponentTypeDescription = "Standard Rectangular Image", PageComponentBody = "<div id=\"component-<componentStamp>-<sectionId>\" class=\"image image-standard\"></div>" });

            if (!componentList.Any(x => x.PageComponentTypeName == "Flex"))
                newComponents.Add(new PageComponentType { PageComponentTypeName = "Flex", PageComponentTypeCategory = PageComponentTypeCategory.Image, PageComponentTypeDescription = "Image That Flexes To Native Size", PageComponentBody = "<img id=\"component-<componentStamp>-<sectionId>\" class=\"image image-auto\" src=\"/Areas/Builder/Content/Images/Sample/small-sample-1.jpg\">" });

            if (!componentList.Any(x => x.PageComponentTypeName == "Container"))
                newComponents.Add(new PageComponentType { PageComponentTypeName = "Container", PageComponentTypeCategory = PageComponentTypeCategory.Bootstrap, PageComponentTypeDescription = "Empty Bootstrap Container", PageComponentBody = "<div id=\"component-<componentStamp>-<sectionId>\" class=\"container component-container\"></div>" });

            if (!componentList.Any(x => x.PageComponentTypeName == "Row"))
                newComponents.Add(new PageComponentType { PageComponentTypeName = "Row", PageComponentTypeCategory = PageComponentTypeCategory.Bootstrap, PageComponentTypeDescription = "Empty Bootstrap Row", PageComponentBody = "<div id=\"component-<componentStamp>-<sectionId>\" class=\"row component-container\"></div>" });

            if (!componentList.Any(x => x.PageComponentTypeName == "<col-lg-2>"))
                newComponents.Add(new PageComponentType { PageComponentTypeName = "<col-lg-2>", PageComponentTypeCategory = PageComponentTypeCategory.Bootstrap, PageComponentTypeDescription = "Empty Bootstrap Column", PageComponentBody = "<div id=\"component-<componentStamp>-<sectionId>\" class=\"col-xs-12 col-sm-6 col-md-4 col-lg-2 component-container\"></div>" });

            if (!componentList.Any(x => x.PageComponentTypeName == "<col-md-3>"))
                newComponents.Add(new PageComponentType { PageComponentTypeName = "<col-md-3>", PageComponentTypeCategory = PageComponentTypeCategory.Bootstrap, PageComponentTypeDescription = "Empty Bootstrap Column", PageComponentBody = "<div id=\"component-<componentStamp>-<sectionId>\" class=\"col-xs-12 col-sm-6 col-md-3 component-container\"></div>" });

            if (!componentList.Any(x => x.PageComponentTypeName == "<col-md-4>"))
                newComponents.Add(new PageComponentType { PageComponentTypeName = "<col-md-4>", PageComponentTypeCategory = PageComponentTypeCategory.Bootstrap, PageComponentTypeDescription = "Empty Bootstrap Column", PageComponentBody = "<div id=\"component-<componentStamp>-<sectionId>\" class=\"col-xs-12 col-sm-6 col-md-4 component-container\"></div>" });

            if (!componentList.Any(x => x.PageComponentTypeName == "<col-md-6>"))
                newComponents.Add(new PageComponentType { PageComponentTypeName = "<col-md-6>", PageComponentTypeCategory = PageComponentTypeCategory.Bootstrap, PageComponentTypeDescription = "Empty Bootstrap Column", PageComponentBody = "<div id=\"component-<componentStamp>-<sectionId>\" class=\"col-xs-12 col-sm-6 component-container\"></div>" });

            if (!componentList.Any(x => x.PageComponentTypeName == "<col-md-12>"))
                newComponents.Add(new PageComponentType { PageComponentTypeName = "<col-md-12>", PageComponentTypeCategory = PageComponentTypeCategory.Bootstrap, PageComponentTypeDescription = "Empty Bootstrap Column", PageComponentBody = "<div id=\"component-<componentStamp>-<sectionId>\" class=\"col-xs-12 component-container\"></div>" });

            if (!componentList.Any(x => x.PageComponentTypeName == "Overlay (Light)"))
                newComponents.Add(new PageComponentType { PageComponentTypeName = "Overlay (Light)", PageComponentTypeCategory = PageComponentTypeCategory.UI, PageComponentTypeDescription = "Light Absolute Overlay", PageComponentBody = "<div id=\"component-<componentStamp>-<sectionId>\" class=\"overlay overlay-light component-container\"></div>" });

            if (!componentList.Any(x => x.PageComponentTypeName == "Overlay (Medium)"))
                newComponents.Add(new PageComponentType { PageComponentTypeName = "Overlay (Medium)", PageComponentTypeCategory = PageComponentTypeCategory.UI, PageComponentTypeDescription = "Medium Absolute Overlay", PageComponentBody = "<div id=\"component-<componentStamp>-<sectionId>\" class=\"overlay overlay-medium component-container\"></div>" });

            if (!componentList.Any(x => x.PageComponentTypeName == "Overlay (Dark)"))
                newComponents.Add(new PageComponentType { PageComponentTypeName = "Overlay (Dark)", PageComponentTypeCategory = PageComponentTypeCategory.UI, PageComponentTypeDescription = "Dark Absolute Overlay", PageComponentBody = "<div id=\"component-<componentStamp>-<sectionId>\" class=\"overlay overlay-dark component-container\"></div>" });

            if (!componentList.Any(x => x.PageComponentTypeName == "Vertical Container"))
                newComponents.Add(new PageComponentType { PageComponentTypeName = "Vertical Container", PageComponentTypeCategory = PageComponentTypeCategory.UI, PageComponentTypeDescription = "Vertically Alligned Container", PageComponentBody = "<div id=\"component-<componentStamp>-<sectionId>\" class=\"vertical-alignment component-container\"></div>" });

            if (!componentList.Any(x => x.PageComponentTypeName == "Glass (Top)"))
                newComponents.Add(new PageComponentType { PageComponentTypeName = "Glass (Top)", PageComponentTypeCategory = PageComponentTypeCategory.UI, PageComponentTypeDescription = "Vertically Alligned Container", PageComponentBody = "<div id=\"glass-top-<componentStamp>-<sectionId>\" class=\"glass glass-top\"></div>" });

            if (!componentList.Any(x => x.PageComponentTypeName == "Glass (Left)"))
                newComponents.Add(new PageComponentType { PageComponentTypeName = "Glass (Left)", PageComponentTypeCategory = PageComponentTypeCategory.UI, PageComponentTypeDescription = "Vertically Alligned Container", PageComponentBody = "<div id=\"glass-left-<componentStamp>-<sectionId>\" class=\"glass glass-left\"></div>" });

            if (!componentList.Any(x => x.PageComponentTypeName == "Glass (Right)"))
                newComponents.Add(new PageComponentType { PageComponentTypeName = "Glass (Right)", PageComponentTypeCategory = PageComponentTypeCategory.UI, PageComponentTypeDescription = "Vertically Alligned Container", PageComponentBody = "<div id=\"glass-right-<componentStamp>-<sectionId>\" class=\"glass glass-right\"></div>" });

            if (!componentList.Any(x => x.PageComponentTypeName == "Glass (Bottom)"))
                newComponents.Add(new PageComponentType { PageComponentTypeName = "Glass (Bottom)", PageComponentTypeCategory = PageComponentTypeCategory.UI, PageComponentTypeDescription = "Vertically Alligned Container", PageComponentBody = "<div id=\"glass-bottom-<componentStamp>-<sectionId>\" class=\"glass glass-bottom\"></div>" });

            if (!componentList.Any(x => x.PageComponentTypeName == "Recent Posts"))
                newComponents.Add(new PageComponentType { PageComponentTypeName = "Recent Posts", PageComponentTypeCategory = PageComponentTypeCategory.Widget, PageComponentTypeDescription = "Vertically Alligned Container", PageComponentBody = "<div id=\"widget-<componentStamp>-<sectionId>\" class=\"widget-wrapper post-list-wrapper\" style=\"text-align: left;\"><div class=\"vertical-alignment\"><div class=\"loading-wrapper\" style=\"text-align: center;\"><div style=\"background-color: black; padding: 10px; display: inline-block;\"><img src=\"/Areas/Builder/Content/Images/Sample/loading-graphic.gif\" /></div></div></div></div>" });

            if (newComponents.Any())
                context.PageComponentTypes.AddRange(newComponents);
        }
    }
}
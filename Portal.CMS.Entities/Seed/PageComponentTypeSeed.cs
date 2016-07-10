using Portal.CMS.Entities.Entities.PageBuilder;
using System.Linq;

namespace Portal.CMS.Entities.Seed
{
    public class PageComponentTypeSeed
    {
        public static void Seed(PortalEntityModel context)
        {
            if (!context.PageComponentTypes.Any(x => x.PageComponentTypeName == "<p>"))
            {
                context.PageComponentTypes.Add(new PageComponentType { PageComponentTypeName = "<p>", PageComponentTypeCategory = PageComponentTypeCategory.Markup, PageComponentTypeDescription = "Single Paragraph Element.", PageComponentBody = "<p id=\"component-<componentStamp>-<sectionId>\">Lorem ipsum dolor sit amet, eu qui accusam scriptorem. Purto erat facilisis sea id, vulputate deseruisse eos te, errem doctus feugiat te prim.</p>" });
            }

            if (!context.PageComponentTypes.Any(x => x.PageComponentTypeName == "<h1>"))
            {
                context.PageComponentTypes.Add(new PageComponentType { PageComponentTypeName = "<h1>", PageComponentTypeCategory = PageComponentTypeCategory.Markup, PageComponentTypeDescription = "Large Title Text Element.", PageComponentBody = "<h1 id=\"component-<componentStamp>-<sectionId>\">Lorem ipsum dolor sit amet, eu qui accusam scriptorem.</h1>" });
            }

            if (!context.PageComponentTypes.Any(x => x.PageComponentTypeName == "<h2>"))
            {
                context.PageComponentTypes.Add(new PageComponentType { PageComponentTypeName = "<h2>", PageComponentTypeCategory = PageComponentTypeCategory.Markup, PageComponentTypeDescription = "Large Title Text Element", PageComponentBody = "<h2 id=\"component-<componentStamp>-<sectionId>\">Lorem ipsum dolor sit amet, eu qui accusam scriptorem.</h2>" });
            }

            if (!context.PageComponentTypes.Any(x => x.PageComponentTypeName == "<h3>"))
            {
                context.PageComponentTypes.Add(new PageComponentType { PageComponentTypeName = "<h3>", PageComponentTypeCategory = PageComponentTypeCategory.Markup, PageComponentTypeDescription = "Title Text Element", PageComponentBody = "<h3 id=\"component-<componentStamp>-<sectionId>\">Lorem ipsum dolor sit amet, eu qui accusam scriptorem.</h3>" });
            }

            if (!context.PageComponentTypes.Any(x => x.PageComponentTypeName == "<h4>"))
            {
                context.PageComponentTypes.Add(new PageComponentType { PageComponentTypeName = "<h4>", PageComponentTypeCategory = PageComponentTypeCategory.Markup, PageComponentTypeDescription = "Title Text Element", PageComponentBody = "<h4 id=\"component-<componentStamp>-<sectionId>\">Lorem ipsum dolor sit amet, eu qui accusam scriptorem.</h4>" });
            }

            if (!context.PageComponentTypes.Any(x => x.PageComponentTypeName == "<a>"))
            {
                context.PageComponentTypes.Add(new PageComponentType { PageComponentTypeName = "<a>", PageComponentTypeCategory = PageComponentTypeCategory.Markup, PageComponentTypeDescription = "Link to Another Page", PageComponentBody = "<a id=\"component-<componentStamp>-<sectionId>\" href=\"#\">Click me to find out more.</a>" });
            }

            if (!context.PageComponentTypes.Any(x => x.PageComponentTypeName == "<btn>"))
            {
                context.PageComponentTypes.Add(new PageComponentType { PageComponentTypeName = "<btn>", PageComponentTypeCategory = PageComponentTypeCategory.Markup, PageComponentTypeDescription = "Button Link to Another Page", PageComponentBody = "<a id=\"component-<componentStamp>-<sectionId>\" href=\"#\" class=\"btn\">Click me to find out more.</a>" });
            }

            if (!context.PageComponentTypes.Any(x => x.PageComponentTypeName == "<img> (Circle)"))
            {
                context.PageComponentTypes.Add(new PageComponentType { PageComponentTypeName = "<img> (Circle)", PageComponentTypeCategory = PageComponentTypeCategory.Control, PageComponentTypeDescription = "Small Circular Image", PageComponentBody = "<div id=\"component-<componentStamp>-<sectionId>\" class=\"image image-circle\" />" });
            }

            if (!context.PageComponentTypes.Any(x => x.PageComponentTypeName == "<img> (Square)"))
            {
                context.PageComponentTypes.Add(new PageComponentType { PageComponentTypeName = "<img> (Square)", PageComponentTypeCategory = PageComponentTypeCategory.Control, PageComponentTypeDescription = "Small Square Image", PageComponentBody = "<div id=\"component-<componentStamp>-<sectionId>\" class=\"image image-square\" />" });
            }

            if (!context.PageComponentTypes.Any(x => x.PageComponentTypeName == "<img> (Icon)"))
            {
                context.PageComponentTypes.Add(new PageComponentType { PageComponentTypeName = "<img> (Icon)", PageComponentTypeCategory = PageComponentTypeCategory.Control, PageComponentTypeDescription = "Tiny Square Image", PageComponentBody = "<div id=\"component-<componentStamp>-<sectionId>\" class=\"image image-icon\" />" });
            }

            if (!context.PageComponentTypes.Any(x => x.PageComponentTypeName == "<img> (Standard)"))
            {
                context.PageComponentTypes.Add(new PageComponentType { PageComponentTypeName = "<img> (Standard)", PageComponentTypeCategory = PageComponentTypeCategory.Control, PageComponentTypeDescription = "Standard Rectangular Image", PageComponentBody = "<div id=\"component-<componentStamp>-<sectionId>\" class=\"image image-standard\" />" });
            }

            if (!context.PageComponentTypes.Any(x => x.PageComponentTypeName == "Container"))
            {
                context.PageComponentTypes.Add(new PageComponentType { PageComponentTypeName = "Container", PageComponentTypeCategory = PageComponentTypeCategory.Bootstrap, PageComponentTypeDescription = "Empty Bootstrap Container", PageComponentBody = "<div id=\"component-<componentStamp>-<sectionId>\" class=\"container component-container\"></div>" });
            }

            if (!context.PageComponentTypes.Any(x => x.PageComponentTypeName == "Row"))
            {
                context.PageComponentTypes.Add(new PageComponentType { PageComponentTypeName = "Row", PageComponentTypeCategory = PageComponentTypeCategory.Bootstrap, PageComponentTypeDescription = "Empty Bootstrap Row", PageComponentBody = "<div id=\"component-<componentStamp>-<sectionId>\" class=\"row component-container\"></div>" });
            }

            if (!context.PageComponentTypes.Any(x => x.PageComponentTypeName == "<col-lg-2>"))
            {
                context.PageComponentTypes.Add(new PageComponentType { PageComponentTypeName = "<col-lg-2>", PageComponentTypeCategory = PageComponentTypeCategory.Bootstrap, PageComponentTypeDescription = "Empty Bootstrap Column", PageComponentBody = "<div id=\"component-<componentStamp>-<sectionId>\" class=\"col-xs-12 col-sm-6 col-md-4 col-lg-2 component-container\"></div>" });
            }

            if (!context.PageComponentTypes.Any(x => x.PageComponentTypeName == "<col-md-3>"))
            {
                context.PageComponentTypes.Add(new PageComponentType { PageComponentTypeName = "<col-md-3>", PageComponentTypeCategory = PageComponentTypeCategory.Bootstrap, PageComponentTypeDescription = "Empty Bootstrap Column", PageComponentBody = "<div id=\"component-<componentStamp>-<sectionId>\" class=\"col-xs-12 col-sm-6 col-md-3 component-container\"></div>" });
            }

            if (!context.PageComponentTypes.Any(x => x.PageComponentTypeName == "<col-md-4>"))
            {
                context.PageComponentTypes.Add(new PageComponentType { PageComponentTypeName = "<col-md-4>", PageComponentTypeCategory = PageComponentTypeCategory.Bootstrap, PageComponentTypeDescription = "Empty Bootstrap Column", PageComponentBody = "<div id=\"component-<componentStamp>-<sectionId>\" class=\"col-xs-12 col-sm-6 col-md-4 component-container\"></div>" });
            }

            if (!context.PageComponentTypes.Any(x => x.PageComponentTypeName == "<col-md-6>"))
            {
                context.PageComponentTypes.Add(new PageComponentType { PageComponentTypeName = "<col-md-6>", PageComponentTypeCategory = PageComponentTypeCategory.Bootstrap, PageComponentTypeDescription = "Empty Bootstrap Column", PageComponentBody = "<div id=\"component-<componentStamp>-<sectionId>\" class=\"col-xs-12 col-sm-6 component-container\"></div>" });
            }

            if (!context.PageComponentTypes.Any(x => x.PageComponentTypeName == "<col-md-12>"))
            {
                context.PageComponentTypes.Add(new PageComponentType { PageComponentTypeName = "<col-md-12>", PageComponentTypeCategory = PageComponentTypeCategory.Bootstrap, PageComponentTypeDescription = "Empty Bootstrap Column", PageComponentBody = "<div id=\"component-<componentStamp>-<sectionId>\" class=\"col-xs-12 component-container\"></div>" });
            }

            if (!context.PageComponentTypes.Any(x => x.PageComponentTypeName == "Overlay (Light)"))
            {
                context.PageComponentTypes.Add(new PageComponentType { PageComponentTypeName = "Overlay (Light)", PageComponentTypeCategory = PageComponentTypeCategory.Control, PageComponentTypeDescription = "Light Absolute Overlay", PageComponentBody = "<div id=\"component-<componentStamp>-<sectionId>\" class=\"overlay overlay-light component-container\"></div>" });
            }

            if (!context.PageComponentTypes.Any(x => x.PageComponentTypeName == "Overlay (Medium)"))
            {
                context.PageComponentTypes.Add(new PageComponentType { PageComponentTypeName = "Overlay (Medium)", PageComponentTypeCategory = PageComponentTypeCategory.Control, PageComponentTypeDescription = "Medium Absolute Overlay", PageComponentBody = "<div id=\"component-<componentStamp>-<sectionId>\" class=\"overlay overlay-medium component-container\"></div>" });
            }

            if (!context.PageComponentTypes.Any(x => x.PageComponentTypeName == "Overlay (Dark)"))
            {
                context.PageComponentTypes.Add(new PageComponentType { PageComponentTypeName = "Overlay (Dark)", PageComponentTypeCategory = PageComponentTypeCategory.Control, PageComponentTypeDescription = "Dark Absolute Overlay", PageComponentBody = "<div id=\"component-<componentStamp>-<sectionId>\" class=\"overlay overlay-dark component-container\"></div>" });
            }

            if (!context.PageComponentTypes.Any(x => x.PageComponentTypeName == "Container (Vertical)"))
            {
                context.PageComponentTypes.Add(new PageComponentType { PageComponentTypeName = "Container (Vertical)", PageComponentTypeCategory = PageComponentTypeCategory.Control, PageComponentTypeDescription = "Vertically Alligned Container", PageComponentBody = "<div id=\"component-<componentStamp>-<sectionId>\" class=\"vertical-alignment component-container\"></div>" });
            }

            if (!context.PageComponentTypes.Any(x => x.PageComponentTypeName == "Glass Border (Top)"))
            {
                context.PageComponentTypes.Add(new PageComponentType { PageComponentTypeName = "Glass Border (Top)", PageComponentTypeCategory = PageComponentTypeCategory.Control, PageComponentTypeDescription = "Vertically Alligned Container", PageComponentBody = "<div id=\"glass-top-<containerId>-<sectionId>\" class=\"glass glass-top\"></div>" });
            }

            if (!context.PageComponentTypes.Any(x => x.PageComponentTypeName == "Glass Border (Left)"))
            {
                context.PageComponentTypes.Add(new PageComponentType { PageComponentTypeName = "Glass Border (Left)", PageComponentTypeCategory = PageComponentTypeCategory.Control, PageComponentTypeDescription = "Vertically Alligned Container", PageComponentBody = "<div id=\"glass-left-<containerId>-<sectionId>\" class=\"glass glass-left\"></div>" });
            }

            if (!context.PageComponentTypes.Any(x => x.PageComponentTypeName == "Glass Border (Right)"))
            {
                context.PageComponentTypes.Add(new PageComponentType { PageComponentTypeName = "Glass Border (Right)", PageComponentTypeCategory = PageComponentTypeCategory.Control, PageComponentTypeDescription = "Vertically Alligned Container", PageComponentBody = "<div id=\"glass-right-<containerId>-<sectionId>\" class=\"glass glass-right\"></div>" });
            }

            if (!context.PageComponentTypes.Any(x => x.PageComponentTypeName == "Glass Border (Bottom)"))
            {
                context.PageComponentTypes.Add(new PageComponentType { PageComponentTypeName = "Glass Border (Bottom)", PageComponentTypeCategory = PageComponentTypeCategory.Control, PageComponentTypeDescription = "Vertically Alligned Container", PageComponentBody = "<div id=\"glass-bottom-<containerId>-<sectionId>\" class=\"glass glass-bottom\"></div>" });
            }
        }
    }
}
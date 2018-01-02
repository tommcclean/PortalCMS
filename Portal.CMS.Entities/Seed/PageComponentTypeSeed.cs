using HtmlAgilityPack;
using Portal.CMS.Entities.Entities;
using Portal.CMS.Entities.Enumerators;
using System.Collections.Generic;
using System.Linq;

namespace Portal.CMS.Entities.Seed
{
    public static class PageComponentTypeSeed
    {
        public static void Seed(PortalEntityModel context)
        {
            var newComponents = new List<PageComponentType>();

            newComponents.Add(new PageComponentType { PageComponentTypeName = "Heading (H1)", PageComponentTypeCategory = PageComponentTypeCategory.Text, PageComponentBody = "<h1 id=\"component-<componentStamp>-<sectionId>\">Large Title</h1>" });
            newComponents.Add(new PageComponentType { PageComponentTypeName = "Heading (H2)", PageComponentTypeCategory = PageComponentTypeCategory.Text, PageComponentBody = "<h2 id=\"component-<componentStamp>-<sectionId>\">Medium Title</h2>" });
            newComponents.Add(new PageComponentType { PageComponentTypeName = "Heading (H3)", PageComponentTypeCategory = PageComponentTypeCategory.Text, PageComponentBody = "<h3 id=\"component-<componentStamp>-<sectionId>\">Small Title</h3>" });
            newComponents.Add(new PageComponentType { PageComponentTypeName = "Heading (H4)", PageComponentTypeCategory = PageComponentTypeCategory.Text, PageComponentBody = "<h4 id=\"component-<componentStamp>-<sectionId>\">Tiny Title</h4>" });
            newComponents.Add(new PageComponentType { PageComponentTypeName = "Paragraph", PageComponentTypeCategory = PageComponentTypeCategory.Text, PageComponentBody = "<p id=\"component-<componentStamp>-<sectionId>\">Standard Paragraph: Lorem ipsum dolor sit amet, eu qui accusam scriptorem. Purto erat facilisis sea id, vulputate deseruisse eos te, errem doctus feugiat te prim.</p>" });
            newComponents.Add(new PageComponentType { PageComponentTypeName = "Link", PageComponentTypeCategory = PageComponentTypeCategory.Text, PageComponentBody = "<a id=\"component-<componentStamp>-<sectionId>\" href=\"#\">Hyperlink: Click me to find out more.</a>" });
            newComponents.Add(new PageComponentType { PageComponentTypeName = "Rounded Button", PageComponentTypeCategory = PageComponentTypeCategory.Text, PageComponentBody = "<a id=\"component-<componentStamp>-<sectionId>\" href=\"#\" class=\"btn\">Rounded Button: Click Me</a>" });
            newComponents.Add(new PageComponentType { PageComponentTypeName = "Square Button", PageComponentTypeCategory = PageComponentTypeCategory.Text, PageComponentBody = "<a id=\"component-<componentStamp>-<sectionId>\" href=\"#\" class=\"btn square\">Square Button: Click Me</a>" });
            newComponents.Add(new PageComponentType { PageComponentTypeName = "Code", PageComponentTypeCategory = PageComponentTypeCategory.Text, PageComponentBody = "<code id=\"component-<componentStamp>-<sectionId>\">Code Block: Lorem ipsum dolor sit amet, eu qui accusam scriptorem. Purto erat facilisis sea id, vulputate deseruisse eos te, errem doctus feugiat te prim.</code>" });
            newComponents.Add(new PageComponentType { PageComponentTypeName = "Freestyle", PageComponentTypeCategory = PageComponentTypeCategory.Text, PageComponentBody = "<div id=\"freestyle-<componentStamp>-<sectionId>\" class=\"freestyle\"><h1>Freestyle</h1><p>Anything goes in a Freestyle Component, the limits are your imagination.</p></div>" });
            newComponents.Add(new PageComponentType { PageComponentTypeName = "FAQ / Spoiler", PageComponentTypeCategory = PageComponentTypeCategory.Text, PageComponentBody = "<div id=\"component-<componentStamp>-<sectionId>\" class=\"component-expand component-container\"><div id=\"component-title-<componentStamp>-<sectionId>\"class=\"component-expand-header\"><h3 id=\"title-<componentStamp>-<sectionId>\">FAQ / Spoiler</h3></div><div id=\"component-expand-body-<componentStamp>-<sectionId>\" class=\"component-body component-container\"><div id=\"freestyle-<componentStamp>-<sectionId>\" class=\"freestyle mce-content-body\"><p>This component is great for building an FAQ Page. Click me to edit this text.</p></div></div></div>" });
            newComponents.Add(new PageComponentType { PageComponentTypeName = "Alert (Danger)", PageComponentTypeCategory = PageComponentTypeCategory.Text, PageComponentBody = "<p class=\"alert alert-danger\">Lorem ipsum dolor sit amet, eu qui accusam scriptorem. Purto erat facilisis sea id, vulputate deseruisse eos te, errem doctus feugiat te prim.</p>" });
            newComponents.Add(new PageComponentType { PageComponentTypeName = "Alert (Warning)", PageComponentTypeCategory = PageComponentTypeCategory.Text, PageComponentBody = "<p class=\"alert alert-warning\">Lorem ipsum dolor sit amet, eu qui accusam scriptorem. Purto erat facilisis sea id, vulputate deseruisse eos te, errem doctus feugiat te prim.</p>" });
            newComponents.Add(new PageComponentType { PageComponentTypeName = "Alert (Info)", PageComponentTypeCategory = PageComponentTypeCategory.Text, PageComponentBody = "<p class=\"alert alert-info\">Lorem ipsum dolor sit amet, eu qui accusam scriptorem. Purto erat facilisis sea id, vulputate deseruisse eos te, errem doctus feugiat te prim.</p>" });
            newComponents.Add(new PageComponentType { PageComponentTypeName = "Carousel (4 Slides)", PageComponentTypeCategory = PageComponentTypeCategory.Text, PageComponentBody = "<div id=\"carousel-<componentStamp>-<sectionId>\" class=\"carousel slide\" data-ride=\"carousel\" data-interval=\"false\"><div id=\"carousel-inner-<componentStamp>-<sectionId>\" class=\"carousel-inner\" role=\"listbox\"><div id=\"item-1-<componentStamp>-<sectionId>\"class=\"item active\"><img id=\"carousel-item-1-<componentStamp>-<sectionId>\" class=\"image image-auto\" src=\"/Areas/PageBuilder/Content/Images/Sample/sample-9.png\"><div id=\"carousel-contaner-1-<componentStamp>-<sectionId>\" class=\"component-container attach-bottom\"><div id=\"freestyle-1-<componentStamp>-<sectionId>\" class=\"freestyle\"><h3 id=\"mce_5\" class=\"mce-content-body\" style=\"position: relative;\"><span style=\"color: #ffffff;\">Lorem Ipsum</span></h3><p id=\"mce_6\" class=\"mce-content-body\" style=\"position: relative;\"><span style=\"color: #ffffff;\">Lorem ipsum dolor sit amet, eu qui accusam scriptorem.</span></p></div></div></div><div id=\"item-2-<componentStamp>-<sectionId>\"class=\"item\"><img id=\"carousel-item-2-<componentStamp>-<sectionId>\" class=\"image image-auto\" src=\"/Areas/PageBuilder/Content/Images/Sample/sample-9.png\"><div id=\"carousel-contaner-2-<componentStamp>-<sectionId>\" class=\"component-container attach-bottom\"><div id=\"freestyle-2-<componentStamp>-<sectionId>\" class=\"freestyle\"><h3 id=\"mce_5\" class=\"mce-content-body\" style=\"position: relative;\"><span style=\"color: #ffffff;\">Lorem Ipsum</span></h3><p id=\"mce_6\" class=\"mce-content-body\" style=\"position: relative;\"><span style=\"color: #ffffff;\">Lorem ipsum dolor sit amet, eu qui accusam scriptorem.</span></p></div></div></div><div id=\"item-3-<componentStamp>-<sectionId>\" class=\"item\"><img id=\"carousel-item-3-<componentStamp>-<sectionId>\" class=\"image image-auto\" src=\"/Areas/PageBuilder/Content/Images/Sample/sample-9.png\"><div id=\"carousel-contaner-3-<componentStamp>-<sectionId>\" class=\"component-container attach-bottom\"><div id=\"freestyle-<componentStamp>-<sectionId>\" class=\"freestyle\"><h3 id=\"mce_5\" class=\"mce-content-body\" style=\"position: relative;\"><span style=\"color: #ffffff;\">Lorem Ipsum</span></h3><p id=\"mce_6\" class=\"mce-content-body\" style=\"position: relative;\"><span style=\"color: #ffffff;\">Lorem ipsum dolor sit amet, eu qui accusam scriptorem.</span></p></div></div></div><div id=\"item-4-<componentStamp>-<sectionId>\" class=\"item\"><img id=\"carousel-item-4-<componentStamp>-<sectionId>\" class=\"image image-auto\" src=\"/Areas/PageBuilder/Content/Images/Sample/sample-9.png\"><div id=\"carousel-contaner-4-<componentStamp>-<sectionId>\" class=\"component-container attach-bottom\"><div id=\"freestyle-<componentStamp>-<sectionId>\" class=\"freestyle\"><h3 id=\"mce_5\" class=\"mce-content-body\" style=\"position: relative;\"><span style=\"color: #ffffff;\">Lorem Ipsum</span></h3><p id=\"mce_6\" class=\"mce-content-body\" style=\"position: relative;\"><span style=\"color: #ffffff;\">Lorem ipsum dolor sit amet, eu qui accusam scriptorem.</span></p></div></div></div></div><div class=\"left carousel-control\" href=\"#element-1-<componentStamp>-<sectionId>\" role=\"button\" data-slide=\"prev\"><span class=\"carousel-arrow glyphicon glyphicon-chevron-left\" aria-hidden=\"true\"></span><span class=\"sr-only\">Previous</span></div><div class=\"right carousel-control\" href=\"#element-1-<componentStamp>-<sectionId>\" role=\"button\" data-slide=\"next\"><span class=\"carousel-arrow glyphicon glyphicon-chevron-right\" aria-hidden=\"true\"></span><span class=\"sr-only\">Next</span></div></div>" });

            newComponents.Add(new PageComponentType { PageComponentTypeName = "Small Round Image", PageComponentTypeCategory = PageComponentTypeCategory.Image, PageComponentBody = "<div id=\"component-<componentStamp>-<sectionId>\" class=\"image image-circle\" style=\"background-image: url('/Areas/PageBuilder/Content/Images/Sample/Sample-4.jpg');\"></div>" });
            newComponents.Add(new PageComponentType { PageComponentTypeName = "Square Image", PageComponentTypeCategory = PageComponentTypeCategory.Image, PageComponentBody = "<div id=\"component-<componentStamp>-<sectionId>\" class=\"image image-square\" style=\"background-image: url('/Areas/PageBuilder/Content/Images/Sample/Sample-5.jpg');\"></div>" });
            newComponents.Add(new PageComponentType { PageComponentTypeName = "Icon", PageComponentTypeCategory = PageComponentTypeCategory.Image, PageComponentBody = "<div id=\"component-<componentStamp>-<sectionId>\" class=\"image image-icon\" style=\"background-image: url('/Areas/PageBuilder/Content/Images/Sample/icon-1.png');\"></div>" });
            newComponents.Add(new PageComponentType { PageComponentTypeName = "Flexible Image", PageComponentTypeCategory = PageComponentTypeCategory.Image, PageComponentBody = "<img id=\"component-<componentStamp>-<sectionId>\" alt=\"Flexible Image\" class=\"image image-auto\" src=\"/Areas/PageBuilder/Content/Images/Sample/small-sample-1.jpg\">" });

            newComponents.Add(new PageComponentType { PageComponentTypeName = "Container", PageComponentTypeCategory = PageComponentTypeCategory.Bootstrap, PageComponentBody = "<div id=\"component-<componentStamp>-<sectionId>\" class=\"container component-container\"></div>" });
            newComponents.Add(new PageComponentType { PageComponentTypeName = "Wide Container", PageComponentTypeCategory = PageComponentTypeCategory.Bootstrap, PageComponentBody = "<div id=\"component-<componentStamp>-<sectionId>\" class=\"container component-container container-wide\"></div>" });
            newComponents.Add(new PageComponentType { PageComponentTypeName = "Row", PageComponentTypeCategory = PageComponentTypeCategory.Bootstrap, PageComponentBody = "<div id=\"component-<componentStamp>-<sectionId>\" class=\"row component-container\"></div>" });
            newComponents.Add(new PageComponentType { PageComponentTypeName = "<col-lg-2>", PageComponentTypeCategory = PageComponentTypeCategory.Bootstrap, PageComponentBody = "<div id=\"component-<componentStamp>-<sectionId>\" class=\"col-xs-12 col-sm-6 col-md-4 col-lg-2 component-container\"></div>" });
            newComponents.Add(new PageComponentType { PageComponentTypeName = "<col-md-3>", PageComponentTypeCategory = PageComponentTypeCategory.Bootstrap, PageComponentBody = "<div id=\"component-<componentStamp>-<sectionId>\" class=\"col-xs-12 col-sm-6 col-md-3 component-container\"></div>" });
            newComponents.Add(new PageComponentType { PageComponentTypeName = "<col-md-4>", PageComponentTypeCategory = PageComponentTypeCategory.Bootstrap, PageComponentBody = "<div id=\"component-<componentStamp>-<sectionId>\" class=\"col-xs-12 col-sm-6 col-md-4 component-container\"></div>" });
            newComponents.Add(new PageComponentType { PageComponentTypeName = "<col-md-6>", PageComponentTypeCategory = PageComponentTypeCategory.Bootstrap, PageComponentBody = "<div id=\"component-<componentStamp>-<sectionId>\" class=\"col-xs-12 col-sm-6 component-container\"></div>" });
            newComponents.Add(new PageComponentType { PageComponentTypeName = "<col-md-8>", PageComponentTypeCategory = PageComponentTypeCategory.Bootstrap, PageComponentBody = "<div id=\"component-<componentStamp>-<sectionId>\" class=\"col-xs-12 col-sm-6 col-md-8 component-container\"></div>" });
            newComponents.Add(new PageComponentType { PageComponentTypeName = "<col-md-12>", PageComponentTypeCategory = PageComponentTypeCategory.Bootstrap, PageComponentBody = "<div id=\"component-<componentStamp>-<sectionId>\" class=\"col-xs-12 component-container\"></div>" });
            newComponents.Add(new PageComponentType { PageComponentTypeName = "Overlay (Light)", PageComponentTypeCategory = PageComponentTypeCategory.UI, PageComponentBody = "<div id=\"component-<componentStamp>-<sectionId>\" class=\"overlay overlay-light component-container\"></div>" });
            newComponents.Add(new PageComponentType { PageComponentTypeName = "Overlay (Medium)", PageComponentTypeCategory = PageComponentTypeCategory.UI, PageComponentBody = "<div id=\"component-<componentStamp>-<sectionId>\" class=\"overlay overlay-medium component-container\"></div>" });
            newComponents.Add(new PageComponentType { PageComponentTypeName = "Overlay (Dark)", PageComponentTypeCategory = PageComponentTypeCategory.UI, PageComponentBody = "<div id=\"component-<componentStamp>-<sectionId>\" class=\"overlay overlay-dark component-container\"></div>" });
            newComponents.Add(new PageComponentType { PageComponentTypeName = "Vertical Container", PageComponentTypeCategory = PageComponentTypeCategory.UI, PageComponentBody = "<div id=\"component-<componentStamp>-<sectionId>\" class=\"vertical-alignment component-container\"></div>" });
            newComponents.Add(new PageComponentType { PageComponentTypeName = "Glass (Top)", PageComponentTypeCategory = PageComponentTypeCategory.UI, PageComponentBody = "<div id=\"glass-top-<componentStamp>-<sectionId>\" class=\"glass glass-top\"></div>" });
            newComponents.Add(new PageComponentType { PageComponentTypeName = "Glass (Left)", PageComponentTypeCategory = PageComponentTypeCategory.UI, PageComponentBody = "<div id=\"glass-left-<componentStamp>-<sectionId>\" class=\"glass glass-left\"></div>" });
            newComponents.Add(new PageComponentType { PageComponentTypeName = "Glass (Right)", PageComponentTypeCategory = PageComponentTypeCategory.UI, PageComponentBody = "<div id=\"glass-right-<componentStamp>-<sectionId>\" class=\"glass glass-right\"></div>" });
            newComponents.Add(new PageComponentType { PageComponentTypeName = "Glass (Bottom)", PageComponentTypeCategory = PageComponentTypeCategory.UI, PageComponentBody = "<div id=\"glass-bottom-<componentStamp>-<sectionId>\" class=\"glass glass-bottom\"></div>" });
            newComponents.Add(new PageComponentType { PageComponentTypeName = "Icon and Text", PageComponentTypeCategory = PageComponentTypeCategory.Text, PageComponentBody = "<table id=\"imagetext-component-<componentStamp>-<sectionId>\" class=\"icon-text component-container\"><tbody id=\"body-<componentStamp>-<sectionId>\"><tr id=\"column-1-<componentStamp>-<sectionId>\"><td id=\"row-<componentStamp>-<sectionId>\" style=\"width: 75px;\"><div id=\"image-<componentStamp>-<sectionId>\"class=\"image image-icon\" style=\"background-image: url('/Areas/PageBuilder/Content/Images/Sample/icon-1.png');'\" /></td><td id=\"column-2-<componentStamp>-<sectionId>\" style=\"text-align: left;\"><h3 id=\"title-<componentStamp>-<sectionId>\"style=\"margin-bottom: 10px;\">Small Title</h3><p id=\"text-<componentStamp>-<sectionId>\">Lorem ipsum dolor sit amet, eu qui.</p></td></tr></tbody></table>" });
            newComponents.Add(new PageComponentType { PageComponentTypeName = "Box (Option 1)", PageComponentTypeCategory = PageComponentTypeCategory.UI, PageComponentBody = "<div id=\"box-<componentStamp>-<sectionId>\" class=\"box box-tall component-container\"><p id=\"title-<componentStamp>-<sectionId>\" class=\"box-title\">My Account</p></div>" });

            newComponents.Add(new PageComponentType { PageComponentTypeName = "Recent Posts (Tiles)", PageComponentTypeCategory = PageComponentTypeCategory.Widget, PageComponentBody = "<div id=\"widget-<componentStamp>-<sectionId>\" class=\"widget-wrapper\" data-area=\"Blog\" data-controller=\"Widgets\" data-action=\"TilesWidget\"></div>" });
            newComponents.Add(new PageComponentType { PageComponentTypeName = "Recent Posts (Retro)", PageComponentTypeCategory = PageComponentTypeCategory.Widget, PageComponentBody = "<div id=\"widget-<componentStamp>-<sectionId>\" class=\"widget-wrapper\" data-area=\"Blog\" data-controller=\"Widgets\" data-action=\"RetroWidget\"></div>" });
            newComponents.Add(new PageComponentType { PageComponentTypeName = "Recent Posts (Boxes)", PageComponentTypeCategory = PageComponentTypeCategory.Widget, PageComponentBody = "<div id=\"widget-<componentStamp>-<sectionId>\" class=\"widget-wrapper\" data-area=\"Blog\" data-controller=\"Widgets\" data-action=\"BoxWidget\"></div>" });
            newComponents.Add(new PageComponentType { PageComponentTypeName = "My Profile Tile", PageComponentTypeCategory = PageComponentTypeCategory.Widget, PageComponentBody = "<div id=\"widget-<componentStamp>-<sectionId>\" class=\"widget-wrapper\" data-area=\"Profile\" data-controller=\"Widgets\" data-action=\"ProfileWidget\"></div>" });
            newComponents.Add(new PageComponentType { PageComponentTypeName = "My Bio Tile", PageComponentTypeCategory = PageComponentTypeCategory.Widget, PageComponentBody = "<div id=\"widget-<componentStamp>-<sectionId>\" class=\"widget-wrapper\" data-area=\"Profile\" data-controller=\"Widgets\" data-action=\"BioWidget\"></div>" });
            newComponents.Add(new PageComponentType { PageComponentTypeName = "Account Security Tile", PageComponentTypeCategory = PageComponentTypeCategory.Widget, PageComponentBody = "<div id=\"widget-<componentStamp>-<sectionId>\" class=\"widget-wrapper\" data-area=\"Profile\" data-controller=\"Widgets\" data-action=\"SecurityWidget\"></div>" });
            newComponents.Add(new PageComponentType { PageComponentTypeName = "Contact Form", PageComponentTypeCategory = PageComponentTypeCategory.Widget, PageComponentBody = "<div id=\"widget-<componentStamp>-<sectionId>\" class=\"widget-wrapper\" data-area=\"Forms\" data-controller=\"ContactWidgets\" data-action=\"SubmitMessageWidget\"></div>" });

            foreach (var component in newComponents)
            {
                component.PageComponentBody = ResolveDuplicateIdentifiers(component.PageComponentBody);
            }

            foreach (var existingComponent in context.PageComponentTypes.ToList())
            {
                var matchedTemplate = newComponents.FirstOrDefault(x => x.PageComponentTypeName == existingComponent.PageComponentTypeName);

                if (matchedTemplate == null) continue;

                if (existingComponent.PageComponentBody != matchedTemplate.PageComponentBody)
                    context.PageComponentTypes.Remove(existingComponent);
                else
                    newComponents.Remove(matchedTemplate);
            }

            context.PageComponentTypes.AddRange(newComponents);
        }

        private static string ResolveDuplicateIdentifiers(string htmlBody)
        {
            var document = new HtmlDocument();

            document.LoadHtml(htmlBody);

            var uniqueElementId = 0;

            foreach (var child in document.DocumentNode.Descendants())
            {
                if (child.NodeType != HtmlNodeType.Element)
                    continue;

                uniqueElementId += 1;
                child.SetAttributeValue("id", $"element-{uniqueElementId}-<componentStamp>-<sectionId>");
            }

            return document.DocumentNode.OuterHtml;
        }
    }
}
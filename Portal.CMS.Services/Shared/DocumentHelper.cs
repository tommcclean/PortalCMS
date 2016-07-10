using HtmlAgilityPack;
using Portal.CMS.Entities.Entities.PageBuilder;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Portal.CMS.Services.Shared
{
    public static class DocumentHelper
    {
        public static HtmlDocument LoadDocument(string htmlBody)
        {
            HtmlDocument htmlDocument = new HtmlDocument();

            htmlDocument.LoadHtml(htmlBody);

            return htmlDocument;
        }

        public static string GetElementContent(string htmlBody, string elementId)
        {
            var document = LoadDocument(htmlBody);

            var element = document.GetElementbyId(elementId);

            return element.InnerHtml;
        }

        public static string AppendComponent(string htmlBody, int pageSectionId, string containerElementId, string componentBody)
        {
            var document = LoadDocument(htmlBody);

            HtmlNode newNode = HtmlNode.CreateNode(componentBody);

            var containerElement = document.GetElementbyId(containerElementId);

            containerElement.AppendChild(newNode);

            return document.DocumentNode.OuterHtml;
        }

        public static string RemoveElementById(string htmlBody, string elementId)
        {
            var document = LoadDocument(htmlBody);

            var element = document.GetElementbyId(elementId);

            element.Remove();

            return document.DocumentNode.OuterHtml;
        }

        public static string UpdateElementContent(string htmlBody, string elementId, string elementValue)
        {
            var document = LoadDocument(htmlBody);

            var element = document.GetElementbyId(elementId);

            element.InnerHtml = elementValue;

            return document.DocumentNode.OuterHtml;
        }

        public static string UpdateElementColour(string htmlBody, string elementId, string elementColour)
        {
            var document = LoadDocument(htmlBody);

            var element = document.GetElementbyId(elementId);

            var colorStyle = string.Format("color: " + elementColour);

            element.SetAttributeValue("style", colorStyle);

            return document.DocumentNode.OuterHtml;
        }

        public static string UpdateElementStyle(string htmlBody, string elementId, string imagePath)
        {
            var document = LoadDocument(htmlBody);

            var element = document.GetElementbyId(elementId);

            var backgroundImageStyle = string.Format("background-image: url('{0}');", imagePath);

            element.SetAttributeValue("style", backgroundImageStyle);

            return document.DocumentNode.OuterHtml;
        }

        public static string UpdateSectionHeight(string htmlBody, string elementId, PageSectionHeight height)
        {
            var document = LoadDocument(htmlBody);

            var element = document.GetElementbyId(elementId);

            var selectedHeight = string.Format("height-{0}", height.ToString()).ToLower();

            var heightClasses = new List<string>() { "height-tall", "height-medium", "height-small", "height-tiny" };

            var classAttribute = element.Attributes.FirstOrDefault(x => x.Name == "class");

            foreach (var heightClass in heightClasses)
                classAttribute.Value = classAttribute.Value.Replace(heightClass, selectedHeight);

            return document.DocumentNode.OuterHtml;
        }

        public static string UpdateBackgroundType(string htmlBody, string elementId, PageSectionBackgroundType backgroundType)
        {
            var document = LoadDocument(htmlBody);

            var element = document.GetElementbyId(elementId);

            var selectedHeight = string.Format("background-{0}", backgroundType.ToString()).ToLower();

            var heightClasses = new List<string>() { "background-static", "background-parallax" };

            var classAttribute = element.Attributes.FirstOrDefault(x => x.Name == "class");

            foreach (var heightClass in heightClasses)
                classAttribute.Value = classAttribute.Value.Replace(heightClass, selectedHeight);

            return document.DocumentNode.OuterHtml;
        }

        public static string ReplaceTokens(string htmlBody, int pageSectionId)
        {
            htmlBody = htmlBody.Replace("<componentStamp>", DateTime.Now.ToString("ddMMyyHHmmss"));
            htmlBody = htmlBody.Replace("<sectionId>", pageSectionId.ToString());

            return htmlBody;
        }

        public static string UpdateElementAttribute(string htmlBody, string elementId, string attributeName, string attributeValue)
        {
            var document = LoadDocument(htmlBody);

            var element = document.GetElementbyId(elementId);

            element.SetAttributeValue(attributeName, attributeValue);

            return document.DocumentNode.OuterHtml;
        }

        public static string GetElementAttribute(string htmlBody, string elementId, string attributeName)
        {
            var document = LoadDocument(htmlBody);

            var element = document.GetElementbyId(elementId);

            return element.GetAttributeValue(attributeName, "");
        }
    }
}
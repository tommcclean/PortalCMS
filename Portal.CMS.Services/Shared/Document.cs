using HtmlAgilityPack;
using Portal.CMS.Entities.Enumerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Portal.CMS.Services.Shared
{
    public class Document
    {
        private HtmlDocument _document { get; set; }

        public string OuterHtml
        {
            get
            {
                return _document.DocumentNode.OuterHtml;
            }
        }

        public Document(string htmlBody)
        {
            _document = new HtmlDocument();

            _document.LoadHtml(htmlBody);
        }

        public void AddElement(string containerElementId, string componentBody)
        {
            var newNode = HtmlNode.CreateNode(componentBody);

            var containerElement = _document.GetElementbyId(containerElementId);

            containerElement.AppendChild(newNode);
        }

        public void UpdateElementContent(string elementId, string elementValue)
        {
            var element = _document.GetElementbyId(elementId);

            element.InnerHtml = elementValue;
        }

        public void UpdateElementAttribute(string elementId, string attributeName, string attributeValue, bool replaceValue)
        {
            var element = _document.GetElementbyId(elementId);

            if (replaceValue)
            {
                element.SetAttributeValue(attributeName, attributeValue);
            }
            else
            {
                var existingAttribute = element.Attributes.SingleOrDefault(x => x.Name == attributeName);
                var existingValue = string.Empty;

                if (existingValue != null)
                    existingValue = existingAttribute.Value;

                element.SetAttributeValue(attributeName, existingValue + attributeValue);
            }
        }

        public void UpdateSectionHeight(string elementId, PageSectionHeight height)
        {
            var element = _document.GetElementbyId(elementId);

            var selectedHeight = string.Format("height-{0}", height).ToLower();

            var heightClasses = new List<string> { "height-tall", "height-medium", "height-small", "height-tiny", "height-standard" };

            var classAttribute = element.Attributes.SingleOrDefault(x => x.Name == "class");

            foreach (var heightClass in heightClasses)
                classAttribute.Value = classAttribute.Value.Replace(heightClass, selectedHeight);
        }

        public void UpdateBackgroundStyle(string elementId, PageSectionBackgroundStyle backgroundStyle)
        {
            var element = _document.GetElementbyId(elementId);

            var selectedHeight = string.Format("background-{0}", backgroundStyle).ToLower();

            var heightClasses = new List<string> { "background-static", "background-parallax" };

            var classAttribute = element.Attributes.SingleOrDefault(x => x.Name == "class");

            foreach (var heightClass in heightClasses)
                classAttribute.Value = classAttribute.Value.Replace(heightClass, selectedHeight);
        }

        public void UpdateBackgroundType(string elementId, bool isPicture)
        {
            var element = _document.GetElementbyId(elementId);

            var selectedBackgroundType = (isPicture ? "background-picture" : "background-colour");

            var classList = new List<string> { "background-picture", "background-colour" };

            var classAttribute = element.Attributes.SingleOrDefault(x => x.Name == "class");

            if (!classAttribute.Value.Contains("background-picture") && !classAttribute.Value.Contains("background-colour"))
                classAttribute.Value = $"{classAttribute.Value} {selectedBackgroundType}";

            foreach (var elementClass in classList)
                classAttribute.Value = classAttribute.Value.Replace(elementClass, selectedBackgroundType);
        }

        public void UpdateAnimation(string elementId, string animation)
        {
            var element = _document.GetElementbyId(elementId);

            var classAttribute = element.Attributes.SingleOrDefault(x => x.Name == "class");

            classAttribute.Value = classAttribute.Value.Replace("animated ", string.Empty);

            var classList = classAttribute.Value.Split(new char[0]).ToList();

            foreach (var animationName in Enum.GetValues(typeof(Animation)))
            {
                var matchedClass = classList.FirstOrDefault(x => animationName.ToString() == x);

                if (matchedClass != null)
                    classList.Remove(matchedClass);
            }

            if (animation.ToLower() != "None")
            {
                classList.Add("animated");
                classList.Add(animation);
            }

            classAttribute.Value = string.Join(" ", classList);
        }

        public void DeleteElement(string elementId)
        {
            var element = _document.GetElementbyId(elementId);

            element.Remove();
        }

        public void CloneElement(int pageSectionId, string elementId, string componentStamp)
        {
            var existingElement = _document.GetElementbyId(elementId);
            var clonedElement = existingElement.Clone();
            var clonedElementContent = clonedElement.OuterHtml;

            clonedElementContent = ResetComponentStamp(clonedElementContent);
            clonedElementContent = ReplaceTokens(clonedElementContent, 0, componentStamp);

            var newnode = HtmlNode.CreateNode(clonedElementContent);
            ApplyUniqueIdentifiers(newnode, pageSectionId, componentStamp);

            existingElement.ParentNode.ChildNodes.Add(clonedElement);
            existingElement.ParentNode.ReplaceChild(newnode, clonedElement);
        }

        public void ApplyUniqueIdentifiers(HtmlNode node, int pageSectionId, string componentStamp)
        {
            var elementCount = 1;

            node.Id = $"element-{elementCount}-{componentStamp}-{pageSectionId}";

            elementCount += 1;

            foreach (var childNode in node.Descendants().Where(x => !string.IsNullOrEmpty(x.Id)))
            {
                childNode.Id = $"element-{elementCount}-{componentStamp}-{pageSectionId}";

                elementCount += 1;
            }
        }

        public static string ResetComponentStamp(string htmlBody)
        {
            htmlBody = Regex.Replace(htmlBody, @"-\d{13}-", "-<componentStamp>-");

            return htmlBody;
        }

        public static string ReplaceTokens(string htmlBody, int pageSectionId, string componentStamp)
        {
            htmlBody = htmlBody.Replace("<componentStamp>", componentStamp);
            htmlBody = htmlBody.Replace("<sectionId>", pageSectionId.ToString());

            return htmlBody;
        }
    }
}
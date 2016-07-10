using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                var existingAttribute = element.Attributes.FirstOrDefault(x => x.Name == attributeName);
                var existingValue = string.Empty;

                if (existingValue != null)
                    existingValue = existingAttribute.Value;

                element.SetAttributeValue(attributeName, existingValue + attributeValue);
            }
        }
    }
}

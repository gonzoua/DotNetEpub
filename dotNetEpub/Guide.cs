using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Epub
{
    class Guide
    {
        private XElement _element;

        internal Guide()
        {
            _element = new XElement(Document.OpfNS + "guide");
        }

        internal void AddReference(string href, string type)
        {
            AddReference(href, type, String.Empty);
        }

        internal void AddReference(string href, string type, string title)
        {
            var itemref = new XElement(Document.OpfNS + "reference",
                new XAttribute("type", type), new XAttribute("title", title));
            if (!String.IsNullOrEmpty(title))
                itemref.SetAttributeValue("title", title);
            _element.Add(itemref);
        }

        internal XElement ToElement()
        {
            return _element;
        }
    }
}

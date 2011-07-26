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

        public Guide()
        {
            _element = new XElement("guide");
        }

        public void AddReference(string href, string type)
        {
            AddReference(href, type, String.Empty);
        }

        public void AddReference(string href, string type, string title)
        {
            var itemref = new XElement("reference", 
                new XAttribute("type", type), new XAttribute("title", title));
            if (!String.IsNullOrEmpty(title))
                itemref.SetAttributeValue("title", title);
            _element.Add(itemref);
        }

        public XElement ToElement()
        {
            return _element;
        }
    }
}

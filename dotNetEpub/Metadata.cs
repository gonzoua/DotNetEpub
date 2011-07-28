using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Epub;
using System.Xml.Linq;

namespace Epub
{
    class Metadata
    {
        private List<DCItem> _dcItems = new List<DCItem>();
        private List<Item> _items = new List<Item>();
        
        public void AddAuthor(string author)
        {
            AddCreator(author, "aut");
        }

        public void AddCreator(string name, string role)
        {
            DCItem dcitem = new DCItem("creator", name);
            dcitem.SetAttribute("role", role);
            _dcItems.Add(dcitem);
        }

        public void AddTitle(string title)
        {
            DCItem dcitem = new DCItem("title", title);
            _dcItems.Add(dcitem);
        }

        public XElement ToElement()
        {
            XNamespace dc = "http://purl.org/dc/elements/1.1/";
            XNamespace opf = "http://www.idpf.org/2007/opf";

            var element = new XElement("metadata",
                new XAttribute(XNamespace.Xmlns + "dc", dc),
                new XAttribute(XNamespace.Xmlns + "opf", opf));

            foreach (Item i in _items) {
                var itemElement = i.ToElement();
                element.Add(itemElement);
            }

            foreach (DCItem i in _dcItems)
            {
                var itemElement = i.ToElement();
                element.Add(itemElement);
            }

            return element;
        }
    }
}

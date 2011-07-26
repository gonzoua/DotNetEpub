using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Epub.Metadata;
using System.Xml.Linq;

namespace Epub
{
    class MetaData
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

        public XElement ToElement()
        {
            var element = new XElement("metadata");

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

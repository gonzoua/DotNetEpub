using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Epub
{
    class Manifest
    {
        XElement _element;
        internal Manifest()
        {
            _element = new XElement(Document.OpfNS + "manifest");
        }

        internal void AddItem(string id, string href, string type)
        {
            XElement item = new XElement(Document.OpfNS + "item");
            item.SetAttributeValue("id", id);
            item.SetAttributeValue("href", href);
            item.SetAttributeValue("media-type", type);
            _element.Add(item);
        }


        internal XElement ToElement()
        {
            return _element;
        }
    }
}

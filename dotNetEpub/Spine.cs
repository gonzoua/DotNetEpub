using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Epub
{
    class Spine
    {
        XElement _element;
        public Spine()
        {
            _element = new XElement("spine");
        }

        public void AddItemRef(string id, bool linear)
        {
            var item = new XElement("itemref", new XAttribute("idref", id));
            if (!linear)
                item.SetAttributeValue("linear", "no");
            _element.Add(item);
        }

        public XElement ToElement()
        {
            return _element;
        }
    }
}

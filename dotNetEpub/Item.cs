using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Epub
{
    class Item
    {
        private string _name;
        private string _value;

        public Item(string name, string value)
        {
            _name = name;
            _value = value;
        }

        public XElement ToElement()
        {
            var element = new XElement("meta");
            element.SetAttributeValue("name", _name);
            element.SetAttributeValue("value", _value);

            return element;
        }
    }
}

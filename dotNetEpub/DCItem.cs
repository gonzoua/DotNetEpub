using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Epub
{
    class DCItem
    {
        private string _name;
        private string _value;

        private IDictionary<string, string> _attributes;
        private IDictionary<string, string> _opfAttributes;

        public DCItem(string name, string value)
        {
            _name = name;
            _value = value;
            _attributes = new Dictionary<string, string>();
            _opfAttributes = new Dictionary<string, string>();

        }

        public void SetAttribute(string name, string value)
        {
            _attributes.Add(name, value);
        }

        public void SetOpfAttribute(string name, string value)
        {
            _opfAttributes.Add(name, value);
        }

        public XElement ToElement()
        {
            XElement element = new XElement(Document.DcNS + _name, _value);
            foreach (string key in _opfAttributes.Keys)
            {
                string value = _opfAttributes[key];
                element.SetAttributeValue(Document.OpfNS + key, value);
            }
            foreach (string key in _attributes.Keys)
            {
                string value = _attributes[key];
                element.SetAttributeValue(key, value);
            }
            return element;
        }
    }
}

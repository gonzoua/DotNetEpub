using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Epub.Metadata
{
    class DCItem
    {
        private string _name;
        private string _value;
        private XElement _element;
        XNamespace dc = "http://purl.org/dc/elements/1.1/";
        XNamespace opf = "http://www.idpf.org/2007/opf";
        private Dictionary<string, string> _attributes = new Dictionary<string, string>();
        public DCItem(string name, string value)
        {
            _name = name;
            _value = value;

            _element = new XElement(dc + _name, _value);
        }

        public void SetAttribute(string name, string value)
        {
            _element.SetAttributeValue(opf + name, value);
        }

        public XElement ToElement()
        {

            return _element;
        }
    }
}

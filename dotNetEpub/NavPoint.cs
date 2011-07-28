using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Epub
{
    public class NavPoint
    {
        private string _label;
        private string _id;
        private string _content;
        private string _class;
        private int _playOrder;
        List<NavPoint> _navpoints;

        public NavPoint(string label, string id, string content, int playOrder, string @class)
        {
            _label = label;
            _id = id;
            _content = content;
            _playOrder = playOrder;
            _class = @class;
            _navpoints = new List<NavPoint>();
        }

        public NavPoint(string label, string id, string content, int playOrder)
            : this(label, id, content, playOrder, String.Empty)
        {

        }

        public NavPoint Add(string label, string id, string content, int playOrder)
        {
            NavPoint n = new NavPoint(label, id, content, playOrder);
            _navpoints.Add(n);
            return n;
        }

        public XElement ToElement()
        {
            XElement e = new XElement("navPoint", new XAttribute("id", _id), new XAttribute("playOrder", _playOrder));
            if (!String.IsNullOrEmpty(_class))
                e.Add(new XAttribute("class", _class));
            e.Add(new XElement("navLabel", new XElement("text", _label)));
            e.Add(new XElement("content", new XAttribute("src", _content)));
            foreach (NavPoint n in _navpoints)
            {
                e.Add(n.ToElement());
            }
            return e;
        }

    }
}

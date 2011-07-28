using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Epub
{
    class NCX
    {
        private string _title;
        private List<String> _authors;
        private string _uid;
        private List<NavPoint> _navpoints;
        private XNamespace NcxNS = "http://www.daisy.org/z3986/2005/ncx/";

        public NCX()
        {
            _navpoints = new List<NavPoint>();
            _authors = new List<string>();
            _title = String.Empty;
        }

        public void SetUid(string uid)
        {
            _uid = uid;
        }

        public void AddAuthor(string author)
        {
            _authors.Add(author);
        }

        public void AddTitle(string title)
        {
            _title += " " + title;
        }

        public void SetTitle(string title)
        {
            if (title == null)
                _title = String.Empty;
            else
                _title = title;
        }

        public XDocument ToXmlDocument()
        {
            XDocument topDoc = new XDocument(
                new XDocumentType("ncx", "-//NISO//DTD ncx 2005-1//EN",
                    "http://www.daisy.org/z3986/2005/ncx-2005-1.dtd", null));
            XElement ncx = new XElement(NcxNS + "ncx");
            ncx.Add(CreateHeadElement());

            // create doc data
            ncx.Add(new XElement(NcxNS + "docTitle",
                new XElement(NcxNS + "text", _title)));

            foreach (string author in _authors)
            {
                ncx.Add(new XElement(NcxNS + "docAuthor",
                    new XElement(NcxNS + "text", author)));
            }

            XElement navMap = new XElement(NcxNS + "navMap");
            foreach (NavPoint n in _navpoints) 
            {
                navMap.Add(n.ToElement());
            }
            ncx.Add(navMap);
            topDoc.Add(ncx);
            return topDoc;
        }

        public NavPoint AddNavPoint(string label, string id, string content, int playOrder)
        {
            NavPoint n = new NavPoint(label, id, content, playOrder);
            _navpoints.Add(n);

            return n;
        }

        private XElement CreateHeadElement()
        {
            XElement head = new XElement(NcxNS + "head");
            head.Add(new XElement(NcxNS + "meta",
                new XAttribute("name", "dtb:uid"),
                new XAttribute("content", _uid)));
            head.Add(new XElement(NcxNS + "meta",
                new XAttribute("name", "dtb:depth"),
                new XAttribute("content", "1")));
            head.Add(new XElement(NcxNS + "meta",
                new XAttribute("name", "dtb:totalPageCount"),
                new XAttribute("content", "0")));
            head.Add(new XElement(NcxNS + "meta",
                new XAttribute("name", "dtb:maxPageNumber"),
                new XAttribute("content", "0")));

            return head;
        }
    }
}

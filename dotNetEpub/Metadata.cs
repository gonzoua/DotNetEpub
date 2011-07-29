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
        
        public void AddAuthor(string name)
        {
            AddCreator(name, "aut");
        }

        public void AddTranslator(string name)
        {
            AddCreator(name, "trl");
        }

        public void AddSubject(string subj)
        {
            DCItem dcitem = new DCItem("subject", subj);
            _dcItems.Add(dcitem);
        }

        public void AddDescription(string description)
        {
            DCItem dcitem = new DCItem("description", description);
            _dcItems.Add(dcitem);
        }

        public void AddType(string @type)
        {
            DCItem dcitem = new DCItem("type", @type);
            _dcItems.Add(dcitem);
        }

        public void AddFormat(string format)
        {
            DCItem dcitem = new DCItem("format", format);
            _dcItems.Add(dcitem);
        }

        public void AddLanguage(string lang)
        {
            DCItem dcitem = new DCItem("language", lang);
            _dcItems.Add(dcitem);
        }

        public void AddRelation(string relation)
        {
            DCItem dcitem = new DCItem("relation", relation);
            _dcItems.Add(dcitem);
        }

        public void AddRights(string rights)
        {
            DCItem dcitem = new DCItem("rights", rights);
            _dcItems.Add(dcitem);
        }

        public void AddCreator(string name, string role)
        {
            DCItem dcitem = new DCItem("creator", name);
            dcitem.SetAttribute("role", role);
            _dcItems.Add(dcitem);
        }

        public void AddCcontributor(string name, string role)
        {
            DCItem dcitem = new DCItem("contributor", name);
            dcitem.SetAttribute("role", role);
            _dcItems.Add(dcitem);
        }

        public void AddTitle(string title)
        {
            DCItem dcitem = new DCItem("title", title);
            _dcItems.Add(dcitem);
        }

        public void AddBookIdentifier(string id, string uuid)
        {
            AddBookIdentifier(id, uuid, string.Empty);
        }

        public void AddBookIdentifier(string id, string uuid, string scheme)
        {
            DCItem dcitem = new DCItem("identifier", uuid);
            dcitem.SetAttribute("id", id);
            if (!String.IsNullOrEmpty(scheme))
                dcitem.SetAttribute("scheme", scheme);
            _dcItems.Add(dcitem);
        }

        public XElement ToElement()
        {
            XNamespace dc = "http://purl.org/dc/elements/1.1/";
            XNamespace opf = "http://www.idpf.org/2007/opf";

            var element = new XElement(Document.OpfNS + "metadata",
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

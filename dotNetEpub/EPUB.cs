using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Xml.Linq;
using Epub;

namespace Epub
{
    public class Document
    {
        private Metadata _metadata;
        private Manifest _manifest;
        private Spine _spine;
        private Guide _guide;
        private NCX _ncx;
        private Dictionary<string, int> _ids;

        public Document()
        {
            _metadata = new Metadata();
            _manifest = new Manifest();
            _spine = new Spine();
            _guide = new Guide();
            _ncx = new NCX();

            _ids = new Dictionary<string, int>();
        }

        private string GetNextId(string kind)
        {
            string id;
            if (_ids.Keys.Contains(kind))
            {
                _ids[kind] += 1;
                id = kind + _ids[kind].ToString();
            }
            else
            {
                id = kind + "1";
                _ids[kind] = 1;
            }

            return id;
        }

        public void AddAuthor(string author)
        {
            _metadata.AddAuthor(author);
        }

        public void Generate()
        {
            NavPoint n = _ncx.AddNavPoint("Chapter1", "html1", "chapter1.xhtml", 1);
            n.Add("Part 1", "html2", "chapter1.xhtml#part1", 2);

            var packageElement = new XElement("package");
            
            packageElement.Add(_metadata.ToElement());
            packageElement.Add(_manifest.ToElement());
            packageElement.Add(_spine.ToElement());
            packageElement.Add(_guide.ToElement());

            Debug.WriteLine(packageElement.ToString());
            Debug.WriteLine("----");
            Debug.WriteLine(_ncx.ToXml());
        }

        public string AddEntry(string path, string type)
        {
            string id = GetNextId("id");
            _manifest.AddItem(id, path, type);
            return id;
        }

        public string AddStylesheetEntry(string path)
        {
            string id = GetNextId("stylesheet");
            _manifest.AddItem(id, path, "text/css");

            return id;
        }

        public string AddXhtmlEntry(string path)
        {
            return AddXhtml(path, true);
        }

        public string AddXhtmlEntry(string path, bool linear)
        {
            string id = GetNextId("html");
            _manifest.AddItem(id, path, "application/xhtml+xml");
            _spine.AddItemRef(id, linear);

            return id;
        }

        public string AddImageEntry(string path)
        {
            string id = GetNextId("img");
            string contentType = String.Empty;
            string lower = path.ToLower();
            if (lower.EndsWith(".jpg") || lower.EndsWith(".jpeg"))
                contentType = "image/jpeg";
            else if (lower.EndsWith(".png"))
                contentType = "image/png";
            else if (lower.EndsWith(".gif"))
                contentType = "image/gif";
            else if (lower.EndsWith(".svg"))
                contentType = "image/svg+xml";
            else
            {
                // TODO: throw exception here?
            }

            _manifest.AddItem(id, path, contentType);

            return id;
        }
    }
}

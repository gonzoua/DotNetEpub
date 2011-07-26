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
        public Document()
        {
            _metadata = new Metadata();
            _manifest = new Manifest();
        }

        public void AddAuthor(string author)
        {
            _metadata.AddAuthor(author);
        }

        public void Generate()
        {
            var packageElement = new XElement("package");
            

            packageElement.Add(_metadata.ToElement());
            packageElement.Add(_manifest.ToElement());

            Debug.WriteLine(packageElement.ToString());
        }

        public void AddCss(string href)
        {
            _manifest.AddItem("css1", href, "text/css");
        }

        public void AddXhtml(string href)
        {
            _manifest.AddItem("html1", href, "application/xhtml+xml");
        }


    }
}

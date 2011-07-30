using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Xml.Linq;
using Epub;
using Ionic.Zip;

namespace Epub
{
    public class Document
    {
        static public XNamespace OpfNS = "http://www.idpf.org/2007/opf";
        static public XNamespace DcNS = "http://purl.org/dc/elements/1.1/";

        private Metadata _metadata;
        private Manifest _manifest;
        private Spine _spine;
        private Guide _guide;
        private NCX _ncx;
        private Container _container;
        private Dictionary<string, int> _ids;

        // several variables is just for convenience
        private string _tempDirectory;
        private string _opfDirectory;
        private string _metainfDirectory;

        public Document()
        {
            _metadata = new Metadata();
            _manifest = new Manifest();
            _spine = new Spine();
            _guide = new Guide();
            _ncx = new NCX();
            _container = new Container();
            _ids = new Dictionary<string, int>();

            // setup mandatory TOC file
            _manifest.AddItem("ncx", "toc.ncx", "application/x-dtbncx+xml");
            _spine.SetToc("ncx");
            _container.AddRootFile("OPF/content.opf", "application/oebps-package+xml");
            Guid guid = Guid.NewGuid();
            string uuid = "urn:uuid:" + guid.ToString();
            _ncx.SetUid(uuid);
            _metadata.AddBookIdentifier("BookId", uuid);
        }

        ~Document()
        {
            if (!String.IsNullOrEmpty(_tempDirectory))
                Directory.Delete(_tempDirectory, true);
        }

        private string GetTempDirectory()
        {
            if (false && String.IsNullOrEmpty(_tempDirectory))
            {
                _tempDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
                Directory.CreateDirectory(_tempDirectory);
            }
            _tempDirectory = "C:\\EPUB\\test.epub";

            return _tempDirectory;
        }

        private string GetOpfDirectory()
        {
            if (String.IsNullOrEmpty(_opfDirectory))
            {
                string tempDirectory = GetTempDirectory();
                _opfDirectory = Path.Combine(tempDirectory, "OPF");
                Directory.CreateDirectory(_opfDirectory);
            }

            return _opfDirectory;
        }

        private string GetMetaInfDirectory()
        {
            if (String.IsNullOrEmpty(_metainfDirectory))
            {
                string tempDirectory = GetTempDirectory();
                _metainfDirectory = Path.Combine(tempDirectory, "META-INF");
                Directory.CreateDirectory(_metainfDirectory);
            }

            return _metainfDirectory;
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
            _ncx.AddAuthor(author);
        }

        public void AddTitle(string title)
        {
            _metadata.AddTitle(title);
            _ncx.AddTitle(title);
        }

        public void AddTranslator(string name)
        {
            _metadata.AddTranslator(name);
        }

        public void AddSubject(string subj)
        {
            _metadata.AddSubject(subj);
        }

        public void AddDescription(string description)
        {
            _metadata.AddDescription(description);
        }

        public void AddType(string @type)
        {
            _metadata.AddType(@type);
        }

        public void AddFormat(string format)
        {
            _metadata.AddFormat(format);
        }

        public void AddLanguage(string lang)
        {
            _metadata.AddLanguage(lang);
        }

        public void AddRelation(string relation)
        {
            _metadata.AddRelation(relation);
        }

        public void AddRights(string rights)
        {
            _metadata.AddRights(rights);
        }

        public void AddBookIdentifier(string id)
        {
            AddBookIdentifier(id, string.Empty);
        }

        public void AddBookIdentifier(string id, string scheme)
        {
            _metadata.AddBookIdentifier(GetNextId("id"), id, scheme);
        }

        public void Generate(string epubFile)
        {
            WriteOpf("content.opf");
            WriteNcx("toc.ncx");
            WriteContainer();

            using (ZipFile zip = new ZipFile())
            {
                zip.EmitTimesInWindowsFormatWhenSaving = false;
                var entry = zip.AddEntry("mimetype", "application/epub+zip", Encoding.ASCII);
                entry.CompressionLevel = Ionic.Zlib.CompressionLevel.None;
                zip.AddDirectory(GetTempDirectory());
                zip.Save(epubFile);
            }
        }

        private string AddEntry(string path, string type)
        {
            string id = GetNextId("id");
            _manifest.AddItem(id, path, type);
            return id;
        }

        private string AddStylesheetEntry(string path)
        {
            string id = GetNextId("stylesheet");
            _manifest.AddItem(id, path, "text/css");

            return id;
        }

        private string AddXhtmlEntry(string path)
        {
            return AddXhtmlEntry(path, true);
        }

        private string AddXhtmlEntry(string path, bool linear)
        {
            string id = GetNextId("html");
            _manifest.AddItem(id, path, "application/xhtml+xml");
            _spine.AddItemRef(id, linear);

            return id;
        }

        private string AddImageEntry(string path)
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

        private void CopyFile(string path, string epubPath)
        {
            string fullPath = Path.Combine(GetOpfDirectory(), epubPath);
            EnsureDirectoryExists(fullPath);
            File.Copy(path, fullPath);
        }

        private string EnsureDirectoryExists(string path)
        {
            // TODO: ensure epubPath contains no ..\..\

            string destDir = Path.GetDirectoryName(path);
            if (!Directory.Exists(destDir))
                Directory.CreateDirectory(destDir);
            return path;
        }

        private void WriteFile(string epubPath, byte[] content)
        {
            string fullPath = Path.Combine(GetOpfDirectory(), epubPath);
            EnsureDirectoryExists(fullPath);
            File.WriteAllBytes(fullPath, content);
        }

        private void WriteFile(string epubPath, string content)
        {
            string fullPath = Path.Combine(GetOpfDirectory(), epubPath);
            EnsureDirectoryExists(fullPath);
            File.WriteAllText(fullPath, content, Encoding.UTF8);
        }


        public string AddImageFile(string path, string epubPath)
        {
            CopyFile(path, epubPath);
            return AddImageEntry(epubPath);
        }

        public string AddStylesheetFile(string path, string epubPath)
        {
            CopyFile(path, epubPath);
            return AddStylesheetEntry(epubPath);
        }

        public string AddXhtmlFile(string path, string epubPath)
        {
 
            return AddXhtmlFile(path, epubPath, false);
        }

        public string AddXhtmlFile(string path, string epubPath, bool linear)
        {
            CopyFile(path, epubPath);
            return AddXhtmlEntry(epubPath, linear);
        }

        public string AddFile(string path, string epubPath, string mediaType)
        {
            CopyFile(path, epubPath);
            return AddEntry(epubPath, mediaType);
        }

        // Data versions of AddNNN functions
        public string AddImageData(string epubPath, byte[] content)
        {
            WriteFile(epubPath, content);
            return AddImageEntry(epubPath);
        }

        public string AddStylesheetData(string epubPath, string content)
        {
            WriteFile(epubPath, content);
            return AddStylesheetEntry(epubPath);
        }

        public string AddXhtmlData(string epubPath, string content, bool linear)
        {
            WriteFile(epubPath, content);
            return AddXhtmlEntry(epubPath, linear);
        }

        public string AddXhtmlData(string epubPath, string content)
        {
            return AddXhtmlData(epubPath, content, false);
        }

        public string AddData(string epubPath, byte[] content, string mediaType)
        {
            WriteFile(epubPath, content);
            return AddEntry(epubPath, mediaType);
        }

        private void WriteOpf(string opfFilePath)
        {
            string fullPath = Path.Combine(GetOpfDirectory(), opfFilePath);

            var packageElement = new XElement(Document.OpfNS + "package", new XAttribute("version", "2.0"), new XAttribute("unique-identifier", "BookId"));

            packageElement.Add(_metadata.ToElement());
            packageElement.Add(_manifest.ToElement());
            packageElement.Add(_spine.ToElement());
            packageElement.Add(_guide.ToElement());

            packageElement.Save(fullPath);
        }

        private void WriteNcx(string ncxFilePath)
        {
            string fullPath = Path.Combine(GetOpfDirectory(), ncxFilePath);
            XDocument ncx = _ncx.ToXmlDocument();
            ncx.Save(fullPath);
        }

        private void WriteContainer()
        {
            string fullPath = Path.Combine(GetMetaInfDirectory(), "container.xml");
            XElement e = _container.ToElement();
            e.Save(fullPath);
        }

        public NavPoint AddNavPoint(string label, string content, int playOrder)
        {
            String id = GetNextId("navid");
            return _ncx.AddNavPoint(label, id, content, playOrder);
        }
    }
}

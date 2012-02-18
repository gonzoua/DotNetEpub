// Copyright (c) 20011 Oleksandr Tymoshenko <gonzo@bluezbox.com>
// All rights reserved.

// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions
// are met:
// 1. Redistributions of source code must retain the above copyright
//    notice, this list of conditions and the following disclaimer.
// 2. Redistributions in binary form must reproduce the above copyright
//    notice, this list of conditions and the following disclaimer in the
//    documentation and/or other materials provided with the distribution.

// THIS SOFTWARE IS PROVIDED BY THE AUTHOR AND CONTRIBUTORS ``AS IS'' AND
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
// ARE DISCLAIMED.  IN NO EVENT SHALL THE AUTHOR OR CONTRIBUTORS BE LIABLE
// FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
// DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS
// OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION)
// HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT
// LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY
// OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF
// SUCH DAMAGE.

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
    /// <summary>
    /// Main class that represents epub file
    /// </summary>
    public class Document
    {
        static internal XNamespace OpfNS = "http://www.idpf.org/2007/opf";
        static internal XNamespace DcNS = "http://purl.org/dc/elements/1.1/";

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
        /// <summary>
        /// Creates new document instance
        /// </summary>
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
        /// <summary>
        /// Destroys instance. Performs temporary directory clean-up if one has been created
        /// </summary>
        ~Document()
        {
            if (!String.IsNullOrEmpty(_tempDirectory) && Directory.Exists(_tempDirectory))
                Directory.Delete(_tempDirectory, true);
        }

        private string GetTempDirectory()
        {
            if (String.IsNullOrEmpty(_tempDirectory))
            {
                _tempDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
                Directory.CreateDirectory(_tempDirectory);
            }

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
        /// <summary>
        /// Add author of the document
        /// </summary>
        /// <param name="author">Human-readable full name</param>
        public void AddAuthor(string author)
        {
            _metadata.AddAuthor(author);
            _ncx.AddAuthor(author);
        }
        /// <summary>
        /// Add title to epub document
        /// </summary>
        /// <param name="title">document's title</param>
        public void AddTitle(string title)
        {
            _metadata.AddTitle(title);
            _ncx.AddTitle(title);
        }
        /// <summary>
        /// Add document translator
        /// </summary>
        /// <param name="name">Human-readable full name</param>
        public void AddTranslator(string name)
        {
            _metadata.AddTranslator(name);
        }
        /// <summary>
        /// Add document subject: phrase or list of keywords
        /// </summary>
        /// <param name="subj">Document's subject</param>
        public void AddSubject(string subj)
        {
            _metadata.AddSubject(subj);
        }
        /// <summary>
        /// Add description of document's content
        /// </summary>
        /// <param name="description">Document description</param>
        public void AddDescription(string description)
        {
            _metadata.AddDescription(description);
        }
        /// <summary>
        /// Add terms describing general categories, functions, genres, or aggregation levels for content. 
        /// The advised best practice is to select a value from a controlled vocabulary.
        /// </summary>
        /// <param name="type">document type</param>
        public void AddType(string @type)
        {
            _metadata.AddType(@type);
        }
        /// <summary>
        /// Add media type or dimensions of the resource. Best practice is to use a value from a controlled vocabulary (e.g. MIME media types).
        /// </summary>
        /// <param name="format">document format</param>
        public void AddFormat(string format)
        {
            _metadata.AddFormat(format);
        }
        /// <summary>
        /// Add a language of the intellectual content of the Publication. 
        /// </summary>
        /// <param name="lang">RFC3066-complient two-letter language code e.g. "en", "es", "it"</param>
        public void AddLanguage(string lang)
        {
            _metadata.AddLanguage(lang);
        }
        /// <summary>
        /// Add an identifier of an auxiliary resource and its relationship to the publication.
        /// </summary>
        /// <param name="relation">document relation</param>
        public void AddRelation(string relation)
        {
            _metadata.AddRelation(relation);
        }
        /// <summary>
        /// Add a statement about rights, or a reference to one.
        /// </summary>
        /// <param name="rights">A statement about rights, or a reference to one</param>
        public void AddRights(string rights)
        {
            _metadata.AddRights(rights);
        }
        /// <summary>
        /// Add book identifier
        /// </summary>
        /// <param name="id">A string or number used to uniquely identify the resource</param>
        public void AddBookIdentifier(string id)
        {
            AddBookIdentifier(id, string.Empty);
        }
        /// <summary>
        /// Add book identifier
        /// </summary>
        /// <param name="id">A string or number used to uniquely identify the resource</param>
        /// <param name="scheme">System or authority that generated or assigned the id parameter, for example "ISBN" or "DOI." </param>
        public void AddBookIdentifier(string id, string scheme)
        {
            _metadata.AddBookIdentifier(GetNextId("id"), id, scheme);
        }
        /// <summary>
        /// Add generic metadata 
        /// </summary>
        /// <param name="name">meta element name</param>
        /// <param name="value">meta element value</param>
        public void AddMetaItem(string name, string value)
        {
            _metadata.AddItem(name, value);
        }

        /// <summary>
        /// Generate document and save to specified filename
        /// </summary>
        /// <param name="epubFile">document's filename</param>
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

        /// <summary>
        /// Add image to document's contents
        /// </summary>
        /// <param name="path">Path to source image file</param>
        /// <param name="epubPath">Path to image file in EPUB</param>
        /// <returns>id of newly created element</returns>
        public string AddImageFile(string path, string epubPath)
        {
            CopyFile(path, epubPath);
            return AddImageEntry(epubPath);
        }
        /// <summary>
        /// Add CSS file to document's contents
        /// </summary>
        /// <param name="path">path to source CSS file</param>
        /// <param name="epubPath">path to destination file in EPUB</param>
        /// <returns>id of newly created element</returns>
        public string AddStylesheetFile(string path, string epubPath)
        {
            CopyFile(path, epubPath);
            return AddStylesheetEntry(epubPath);
        }
        /// <summary>
        /// Add primary XHTML file to document's contents
        /// </summary>
        /// <param name="path">path to source file</param>
        /// <param name="epubPath">path in EPUB</param>
        /// <returns>id of newly created element</returns>
        public string AddXhtmlFile(string path, string epubPath)
        {
 
            return AddXhtmlFile(path, epubPath, true);
        }
        /// <summary>
        /// Add primary or auxiliary (like notes) XHTML file to document's content
        /// </summary>
        /// <param name="path">path to source file</param>
        /// <param name="epubPath">path in epub</param>
        /// <param name="primary">true for primary document, false for auxiliary</param>
        /// <returns>id of newly created element</returns>
        public string AddXhtmlFile(string path, string epubPath, bool primary)
        {
            CopyFile(path, epubPath);
            return AddXhtmlEntry(epubPath, primary);
        }
        /// <summary>
        /// Add generic file to document's contents
        /// </summary>
        /// <param name="path">source file path</param>
        /// <param name="epubPath">path in EPUB</param>
        /// <param name="mediaType">MIME media-type, e.g. "application/octet-stream"</param>
        /// <returns>id of newly added file</returns>
        public string AddFile(string path, string epubPath, string mediaType)
        {
            CopyFile(path, epubPath);
            return AddEntry(epubPath, mediaType);
        }

        // Data versions of AddNNN functions
        /// <summary>
        /// Add image file to document with specified content. Image type 
        /// is detected by filename's extension
        /// </summary>
        /// <param name="epubPath">path in EPUB</param>
        /// <param name="content">file content</param>
        /// <returns>id of newly added file</returns>
        public string AddImageData(string epubPath, byte[] content)
        {
            WriteFile(epubPath, content);
            return AddImageEntry(epubPath);
        }
        /// <summary>
        /// Add CSS file to document with specified content.
        /// </summary>
        /// <param name="epubPath">path in EPUB</param>
        /// <param name="content">file content</param>
        /// <returns>id of newly added file</returns>
        public string AddStylesheetData(string epubPath, string content)
        {
            WriteFile(epubPath, content);
            return AddStylesheetEntry(epubPath);
        }
        /// <summary>
        /// Add primary or auxiliary XHTML file to document with specified content.
        /// </summary>
        /// <param name="epubPath">path in EPUB</param>
        /// <param name="content">file content</param>
        /// <param name="primary">true if file is primary, false if auxiliary</param>
        /// <returns>identifier of added file</returns>
        public string AddXhtmlData(string epubPath, string content, bool primary)
        {
            WriteFile(epubPath, content);
            return AddXhtmlEntry(epubPath, primary);
        }
        /// <summary>
        /// Add primary  XHTML file to document with specified content.
        /// </summary>
        /// <param name="epubPath">path in EPUB</param>
        /// <param name="content">file contents</param>
        /// <returns>identifier of added file</returns>
        public string AddXhtmlData(string epubPath, string content)
        {
            return AddXhtmlData(epubPath, content, true);
        }
        /// <summary>
        /// Add generic file to document with specified content
        /// </summary>
        /// <param name="epubPath">path in EPUB</param>
        /// <param name="content">file content</param>
        /// <param name="mediaType">MIME media-type, e.g. "application/octet-stream"</param>
        /// <returns>identifier of added file</returns>
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
        /// <summary>
        /// Add navigation point to top-level Table of Contents. 
        /// </summary>
        /// <param name="label">Text of TOC entry</param>
        /// <param name="content">Link to TOC entry</param>
        /// <param name="playOrder">play order counter</param>
        /// <returns>newly created NavPoint </returns>
        public NavPoint AddNavPoint(string label, string content, int playOrder)
        {
            String id = GetNextId("navid");
            return _ncx.AddNavPoint(label, id, content, playOrder);
        }
    }
}

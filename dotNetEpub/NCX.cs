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
using System.Xml.Linq;

namespace Epub
{
    class NCX
    {
        private string _title;
        private List<String> _authors;
        private string _uid;
        private List<NavPoint> _navpoints;
        internal static XNamespace NcxNS = "http://www.daisy.org/z3986/2005/ncx/";

        internal NCX()
        {
            _navpoints = new List<NavPoint>();
            _authors = new List<string>();
            _title = String.Empty;
        }

        internal void SetUid(string uid)
        {
            _uid = uid;
        }

        internal void AddAuthor(string author)
        {
            _authors.Add(author);
        }

        internal void AddTitle(string title)
        {
            _title += " " + title;
        }

        internal void SetTitle(string title)
        {
            if (title == null)
                _title = String.Empty;
            else
                _title = title;
        }

        internal XDocument ToXmlDocument()
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

        internal NavPoint AddNavPoint(string label, string id, string content, int playOrder)
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

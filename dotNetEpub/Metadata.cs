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
using Epub;
using System.Xml.Linq;

namespace Epub
{
    class Metadata
    {
        private List<DCItem> _dcItems = new List<DCItem>();
        private List<Item> _items = new List<Item>();
        
        internal void AddAuthor(string name)
        {
            AddCreator(name, "aut");
        }

        internal void AddTranslator(string name)
        {
            AddCreator(name, "trl");
        }

        internal void AddSubject(string subj)
        {
            DCItem dcitem = new DCItem("subject", subj);
            _dcItems.Add(dcitem);
        }

        internal void AddDescription(string description)
        {
            DCItem dcitem = new DCItem("description", description);
            _dcItems.Add(dcitem);
        }

        internal void AddType(string @type)
        {
            DCItem dcitem = new DCItem("type", @type);
            _dcItems.Add(dcitem);
        }

        internal void AddFormat(string format)
        {
            DCItem dcitem = new DCItem("format", format);
            _dcItems.Add(dcitem);
        }

        internal void AddLanguage(string lang)
        {
            DCItem dcitem = new DCItem("language", lang);
            _dcItems.Add(dcitem);
        }

        internal void AddRelation(string relation)
        {
            DCItem dcitem = new DCItem("relation", relation);
            _dcItems.Add(dcitem);
        }

        internal void AddRights(string rights)
        {
            DCItem dcitem = new DCItem("rights", rights);
            _dcItems.Add(dcitem);
        }

        internal void AddCreator(string name, string role)
        {
            DCItem dcitem = new DCItem("creator", name);
            dcitem.SetOpfAttribute("role", role);
            _dcItems.Add(dcitem);
        }

        internal void AddCcontributor(string name, string role)
        {
            DCItem dcitem = new DCItem("contributor", name);
            dcitem.SetOpfAttribute("role", role);
            _dcItems.Add(dcitem);
        }

        internal void AddTitle(string title)
        {
            DCItem dcitem = new DCItem("title", title);
            _dcItems.Add(dcitem);
        }

        internal void AddBookIdentifier(string id, string uuid)
        {
            AddBookIdentifier(id, uuid, string.Empty);
        }

        internal void AddBookIdentifier(string id, string uuid, string scheme)
        {
            DCItem dcitem = new DCItem("identifier", uuid);
            dcitem.SetAttribute("id", id);
            if (!String.IsNullOrEmpty(scheme))
                dcitem.SetOpfAttribute("scheme", scheme);
            _dcItems.Add(dcitem);
        }

        internal XElement ToElement()
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

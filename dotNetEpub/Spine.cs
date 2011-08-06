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
    class Spine
    {
        private struct ItemRef
        {
            public string id;
            public bool linear;
        };


        private string _toc;
        private List<ItemRef> _itemRefs;

        internal Spine()
        {
            _itemRefs = new List<ItemRef>();
        }

        internal void SetToc(string toc)
        {
            _toc = toc;
        }

        internal void AddItemRef(string id, bool linear)
        {
            ItemRef r;
            r.id = id;
            r.linear = linear;
            _itemRefs.Add(r);
        }

        internal XElement ToElement()
        {
            XElement element = new XElement(Document.OpfNS + "spine");
            if (!String.IsNullOrEmpty(_toc))
                element.Add(new XAttribute("toc", _toc));
            foreach (ItemRef r in _itemRefs)
            {
                var item = new XElement(Document.OpfNS + "itemref", new XAttribute("idref", r.id));
                if (!r.linear)
                    item.SetAttributeValue("linear", "no");
                element.Add(item);
            }
            return element;
        }
    }
}

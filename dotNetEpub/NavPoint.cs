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
    /// <summary>
    /// Class for TOC entry. Top-level navPoints should be created by Epub.Document.AddNavPoint method
    /// </summary>
    public class NavPoint
    {
        private string _label;
        private string _id;
        private string _content;
        private string _class;
        private int _playOrder;
        List<NavPoint> _navpoints;

        internal NavPoint(string label, string id, string content, int playOrder, string @class)
        {
            _label = label;
            _id = id;
            _content = content;
            _playOrder = playOrder;
            _class = @class;

            _navpoints = new List<NavPoint>();
        }

        internal NavPoint(string label, string id, string content, int playOrder)
            : this(label, id, content, playOrder, String.Empty)
        {

        }

        /// <summary>
        /// Add TOC entry as a direct child of this NavPoint
        /// </summary>
        /// <param name="label">Text of TOC entry</param>
        /// <param name="content">Link to TOC entry</param>
        /// <param name="playOrder">play order counter</param>
        /// <returns>newly created NavPoint </returns>
        public NavPoint AddNavPoint(string label, string content, int playOrder)
        {
            string id = _id + "x" + (_navpoints.Count + 1).ToString();
 
            NavPoint n = new NavPoint(label, id, content, playOrder);
            _navpoints.Add(n);
            return n;
        }

        internal XElement ToElement()
        {
            XElement e = new XElement(NCX.NcxNS + "navPoint", new XAttribute("id", _id), new XAttribute("playOrder", _playOrder));
            if (!String.IsNullOrEmpty(_class))
                e.Add(new XAttribute("class", _class));
            e.Add(new XElement(NCX.NcxNS + "navLabel", new XElement(NCX.NcxNS + "text", _label)));
            e.Add(new XElement(NCX.NcxNS + "content", new XAttribute("src", _content)));
            foreach (NavPoint n in _navpoints)
            {
                e.Add(n.ToElement());
            }
            return e;
        }

    }
}

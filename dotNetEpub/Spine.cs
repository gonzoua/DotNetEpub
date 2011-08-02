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

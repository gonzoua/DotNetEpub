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

        public Spine()
        {
            _itemRefs = new List<ItemRef>();
        }

        public void SetToc(string toc)
        {
            _toc = toc;
        }

        public void AddItemRef(string id, bool linear)
        {
            ItemRef r;
            r.id = id;
            r.linear = linear;
            _itemRefs.Add(r);
        }

        public XElement ToElement()
        {
            XElement element = new XElement(Document.OpfNS + "spine");
            if (String.IsNullOrEmpty(_toc))
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

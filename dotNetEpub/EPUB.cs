using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Epub;

namespace Epub
{
    public class Document
    {
        private MetaData _metadata = new MetaData();

        public void AddAuthor(string author)
        {
            _metadata.AddAuthor(author);
        }

        public void Generate()
        {
            var element = _metadata.ToElement();
            Debug.WriteLine(element.ToString());
        }
    }
}

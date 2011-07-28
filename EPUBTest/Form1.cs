using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EPubDocument = Epub.Document;
using NavPoint = Epub.NavPoint;

namespace EPUBTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void testButton_Click(object sender, EventArgs e)
        {
            var epub = new EPubDocument();
            epub.AddAuthor("Jack London");
            epub.AddTitle("White Fang");
            epub.AddStylesheetFile("style.css", "style.css");
            string id1 = epub.AddXhtmlFile("page0001.xhtml", "page0001.xhtml");
            string id2 = epub.AddXhtmlFile("page0002.xhtml", "page0002.xhtml");
            string id3 = epub.AddXhtmlFile("page0003.xhtml", "page0003.xhtml");
            epub.AddNavPoint("Chapter1", id1, "page0001.xhtml", 1);
            epub.AddNavPoint("Chapter2", id2, "page0002.xhtml", 2);
            epub.AddNavPoint("Chapter3", id3, "page0003.xhtml", 3);

            epub.Generate("C:\\EPUB\\x.epub");
        }
    }
}

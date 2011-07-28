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
            epub.AddStylesheetFile("style.css", "style.css");
            epub.AddXhtmlFile("page1.xhtml", "page1.xhtml");
            NavPoint n = epub.AddNavPoint("Chapter1", "html1", "chapter1.xhtml", 1);
            n.Add("Part 1", "html2", "chapter1.xhtml#part1", 2);

            epub.Generate();
        }
    }
}

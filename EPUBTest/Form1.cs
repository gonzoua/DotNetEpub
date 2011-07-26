using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EPubDocument = Epub.Document;

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
            epub.AddCss("style.css");
            epub.AddXhtml("page1.xhtml");
            epub.Generate();
        }
    }
}

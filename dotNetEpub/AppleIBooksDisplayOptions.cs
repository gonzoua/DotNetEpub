using System.Xml.Linq;

namespace Epub
{
    class AppleIBooksDisplayOptions
    {
        internal XElement ToElement()
        {
            XElement element = new XElement("display_options",
                new XElement("platform",
                    new XAttribute("name", "*"),
                    new XElement("option",
                        new XAttribute("name", "specified-fonts"),
                        true)));

            return element;
        }
    }
}

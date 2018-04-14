using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PDFBuilder.Models
{
    [Serializable]
    internal class Body
    {
        [XmlElement(typeof(Paragraph), ElementName = "p")]
        internal List<Element> Elements { get; set; }
    }
}

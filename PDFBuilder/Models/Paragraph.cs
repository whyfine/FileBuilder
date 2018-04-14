using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PDFBuilder.Models
{
    [Serializable]
    internal class Paragraph : Element
    {
        [XmlAttribute("txt")]
        internal string Txt { get; set; }
        [XmlText]
        internal string Content { get; set; }
        internal override Element Child { get; set; }
        internal override List<Element> Childs { get; set; }
    }
}

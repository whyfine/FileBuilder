using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PDFBuilder.Models
{
    [Serializable]
    public class PDFParagraph : PDFElement
    {
        [XmlAttribute("txt")]
        public string Txt { get; set; }
        [XmlText]
        public string Content { get; set; }
        public override PDFElement Child { get; set; }
        public override List<PDFElement> Childs { get; set; }
    }
}

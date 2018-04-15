using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FileBuilder.Models
{
    [Serializable]
    public class FileSpan: FileElement
    {
        [XmlText]
        public string Content { get; set; }
        [XmlAttribute("color")]
        public string Color { get; set; }
        [XmlAttribute("height")]
        public string Height { get; set; }
        [XmlAttribute("fontName")]
        public string FontName { get; set; }

        [XmlAttribute("fontStyle")]
        public string FontStyle { get; set; }
        [XmlAttribute("fontSize")]
        public string FontSize { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TYFileBuilder.Models
{
    [Serializable]
    public class FileFont : FileElement
    {

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

        [XmlAttribute("backgroundColor")]
        public string BackgroundColor { get; set; }
    }
}

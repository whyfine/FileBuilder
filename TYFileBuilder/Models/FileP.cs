using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TYFileBuilder.Models
{
    [Serializable]
    public class FileP : FileElement
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
        [XmlAttribute("align")]
        public string Align { get; set; }
        [XmlAttribute("top")]
        public string Top { get; set; }
        [XmlAttribute("left")]
        public string Left { get; set; }
        [XmlAttribute("right")]
        public string Right { get; set; }
        [XmlAttribute("spacing")]
        public string Spacing { get; set; }
    }
}

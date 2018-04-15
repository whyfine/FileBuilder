using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FileBuilder.Models
{
    [Serializable]
    public class FileP : FileElement
    {
        [XmlText]
        public string Content { get; set; }
        [XmlAttribute("fontName")]
        public string FontName { get; set; }
        [XmlAttribute("fontSize")]
        public string FontSize { get; set; }
        public List<FileElement> Childs { get; set; }
    }
}

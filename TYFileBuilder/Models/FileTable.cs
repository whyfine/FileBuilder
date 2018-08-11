using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TYFileBuilder.Models
{
    [Serializable]
    public class FileTable: FileElement
    {
        [XmlElement(ElementName = "tr")]
        public List<FileTableRow> Rows { get; set; }
        [XmlElement("width")]
        public string Width { get; set; }
        [XmlElement("align")]
        public string Align { get; set; }
    }
    [Serializable]
    public class FileTableRow
    {
        [XmlElement(ElementName = "td")]
        public List<FileTableCell> Cells { get; set; }
    }
    [Serializable]
    public class FileTableCell : FileElement
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
    }
}

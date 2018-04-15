using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FileBuilder.Models
{
    [Serializable]
    public class FileTable: FileElement
    {
        [XmlElement(ElementName = "tr")]
        public List<FileTableRow> Rows { get; set; }
    }
    [Serializable]
    public class FileTableRow
    {
        [XmlElement(ElementName = "td")]
        public List<FileTableCell> Cells { get; set; }
    }
    [Serializable]
    public class FileTableCell
    {

    }
}

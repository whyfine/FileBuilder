using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FileBuilder.Models
{
    [Serializable]
    public class FileList: FileElement
    {
        public List<FileListItem> Items { get; set; }
    }

    [Serializable]
    public class FileListItem
    {

        [XmlText]
        public string Content { get; set; }
    }
}

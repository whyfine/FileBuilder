using TYFileBuilder.Adapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TYFileBuilder.Models
{
    [Serializable]
    [XmlRoot("file")]
    public class FileDocument : FileElement
    {
        [XmlAttribute("pageSize")]
        public string PageSize { get; set; }
        [XmlAttribute("backgroundColor")]
        public string BackgroundColor { get; set; }
        [XmlAttribute("margin")]
        public string Margin { get; set; }

        [XmlAttribute("title")]
        public string Title { get; set; }
        [XmlAttribute("subject")]
        public string Subject { get; set; }
        [XmlAttribute("keywords")]
        public string Keywords { get; set; }
        [XmlAttribute("author")]
        public string Author { get; set; }
        [XmlAttribute("creator")]
        public string Creator { get; set; }
        [XmlAttribute("producer")]
        public string Producer { get; set; }
        [XmlAttribute("createDate")]
        public string CreationDate { get; set; }
        //public object Header { get; set; }
        [XmlElement(ElementName = "body")]
        public FileBody Body { get; set; }
        //public object Footer { get; set; }
    }
}

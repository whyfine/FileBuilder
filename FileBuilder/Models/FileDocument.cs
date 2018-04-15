using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FileBuilder.Models
{
    [Serializable]
    [XmlRoot("pdf")]
    public class FileDocument
    {
        [XmlAttribute("pageSize")]
        public string PageSize { get; set; }
        [XmlAttribute("backgroundColor")]
        public string BackgroundColor { get; set; }
        [XmlAttribute("margin")]
        public string Margin { get; set; }

        [XmlAttribute("title")]
        public string Title { get; set; }
        [XmlAttribute]
        public string Subject { get; set; }
        [XmlAttribute]
        public string Keywords { get; set; }
        [XmlAttribute]
        public string Author { get; set; }
        [XmlAttribute]
        public string Creator { get; set; }
        [XmlAttribute]
        public string Producer { get; set; }
        [XmlAttribute]
        public string CreationDate { get; set; }
        //public object Header { get; set; }
        [XmlElement(ElementName = "body")]
        public FileBody Body { get; set; }
        //public object Footer { get; set; }
    }
}

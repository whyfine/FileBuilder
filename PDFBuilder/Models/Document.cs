using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PDFBuilder.Models
{
    [Serializable]
    [XmlRoot("pdf")]
    internal class Document
    {
        [XmlAttribute("title")]
        internal string Title { get; set; }
        [XmlAttribute]
        internal string Subject { get; set; }
        [XmlAttribute]
        internal string Keywords { get; set; }
        [XmlAttribute]
        internal string Author { get; set; }
        [XmlAttribute]
        internal string Creator { get; set; }
        [XmlAttribute]
        internal string Producer { get; set; }
        [XmlAttribute]
        internal string CreationDate { get; set; }
        //internal object Header { get; set; }
        [XmlElement(ElementName = "body")]
        internal Body Body { get; set; }
        //internal object Footer { get; set; }
    }
}

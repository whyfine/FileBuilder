using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TYFileBuilder.Models
{
    [Serializable]
    public abstract class FileElement
    {
        [XmlElement(typeof(FileTable), ElementName = "table")]
        [XmlElement(typeof(FileP), ElementName = "p")]
        [XmlElement(typeof(FileSpan), ElementName = "span")]
        [XmlElement(typeof(FileFont), ElementName = "font")]
        public List<FileElement> Childs { get; set; }

        [XmlAttribute("style")]
        public string Style
        {
            set
            {
                this.ElementStyle = new FileElementStyle();
                if (value == null)
                    return;
                var width = Regex.Match(value, @"width:(\d+?)px;");
                float f;
                if (width.Success)
                {
                    if (float.TryParse(width.Groups[1].Value, out f))
                        this.ElementStyle.Width = f;
                }
                var heigth= Regex.Match(value, @"height:(\d+?)px;");
                if (heigth.Success)
                {
                    if (float.TryParse(heigth.Groups[1].Value, out f))
                        this.ElementStyle.Height = f;
                }
            }
        }

        [XmlIgnore]
        public FileElementStyle ElementStyle { get; set; }

        [XmlText]
        public string Content { get; set; }
    }
}

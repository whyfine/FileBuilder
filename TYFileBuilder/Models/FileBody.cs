﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TYFileBuilder.Models
{
    [Serializable]
    public class FileBody
    {
        [XmlElement(typeof(FileTable), ElementName = "table")]
        [XmlElement(typeof(FileP), ElementName = "p")]
        [XmlElement(typeof(FileSpan), ElementName = "span")]
        [XmlElement(typeof(FileFont), ElementName = "font")]
        public List<FileElement> Elements { get; set; }
    }
}

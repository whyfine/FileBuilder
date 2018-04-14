using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PDFBuilder.Models
{
    [Serializable]
    public abstract class PDFElement
    {

        public abstract PDFElement Child { get; set; }
        public abstract List<PDFElement> Childs { get; set; }
    }
}

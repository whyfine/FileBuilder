using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PDFBuilder.Models
{
    [Serializable]
    internal abstract class Element
    {

        internal abstract Element Child { get; set; }
        internal abstract List<Element> Childs { get; set; }
    }
}

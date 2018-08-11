using TYFileBuilder.Adapter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TYFileBuilder.Models
{
    public class FileContext
    {
        public object XmlSource { get; set; }
        internal FileDocument Document { get; set; }
        public Stream Stream { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FileBuilder.Models
{
    public class FileContext
    {
        public MemoryStream File { get; set; }
        public FileDocument Document { get; set; }
    }
}

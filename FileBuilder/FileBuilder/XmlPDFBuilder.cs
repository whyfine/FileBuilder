using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileBuilder
{
    public class XmlPDFBuilder : BaseFileBuilder, IPDFBuilder
    {
        protected override IFilePainter GetFilePainter()
        {
            return new iTextSharpPDFPainter();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileBuilder
{
    public class XmlFileBuilder : BaseFileBuilder, IPDFBuilder
    {
        protected override IFilePainter GetFilePainter()
        {
            return new ITextSharpPDFPainter();
        }
    }
}

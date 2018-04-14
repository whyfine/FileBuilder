using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFBuilder
{
    public class XmlPDFBuilder : BasePDFBuilder, IPDFBuilderAsync
    {
        protected override IPDFPainter GetPDFPainter()
        {
            return new ITextSharpPDFPainter();
        }
    }
}

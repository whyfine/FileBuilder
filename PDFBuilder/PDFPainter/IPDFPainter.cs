using PDFBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFBuilder
{
    public interface IPDFPainter
    {
        void InitDocument(PDFDocument document);
        void DrawingBody(PDFBody body);
    }
}

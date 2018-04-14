using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFBuilder
{
    public abstract class BasePDFBuilder : IPDFBuilderAsync
    {
        public byte[] Builder(IRenderer renderer)
        {
            throw new NotImplementedException();
        }

        public void Builder(string savePath, IRenderer renderer)
        {
            var pdfDocument = renderer.Render();
            var pdfPainter = this.GetPDFPainter();
            pdfPainter.InitDocument(pdfDocument);
            pdfPainter.DrawingBody(pdfDocument.Body);
        }

        public Task<byte[]> BuilderAsync(IRenderer renderer)
        {
            throw new NotImplementedException();
        }

        public Task BuilderAsync(string path, IRenderer renderer)
        {
            throw new NotImplementedException();
        }

        protected abstract IPDFPainter GetPDFPainter();
    }
}

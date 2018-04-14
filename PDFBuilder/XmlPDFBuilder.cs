using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PDFBuilder
{
    public class XmlPDFBuilder : IPDFBuilderAsync
    {
        public byte[] Builder(IRenderer renderer)
        {
            var xml = renderer.Render();

            return null;
        }

        public void Builder(string savePath, IRenderer renderer)
        {
            var xml = renderer.Render();
            var xmlSerializer = new XmlSerializer(typeof(PDFBuilder.Models.Document));
            var xmlDoc = xmlSerializer.Deserializer<PDFBuilder.Models.Document>(xml);
            var pdfDoc = new iTextSharp.text.Document();
            PdfWriter.GetInstance(pdfDoc, new FileStream(savePath, FileMode.Create));
            foreach (var element in xmlDoc.Body.Elements)
            {
                var p = element as PDFBuilder.Models.Paragraph;
                pdfDoc.Add(new Paragraph(p.Content));
            }
            pdfDoc.Close();
        }

        public async Task<byte[]> BuilderAsync(IRenderer renderer)
        {
            throw new NotImplementedException();
        }

        public async Task BuilderAsync(string path, IRenderer renderer)
        {
            throw new NotImplementedException();
        }
    }
}

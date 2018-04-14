using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using PDFBuilder.Models;

namespace PDFBuilder
{
    internal class ITextSharpPDFPainter : IPDFPainter
    {
        public void InitDocument(PDFDocument document)
        {
            var pageSize = this.GetPageSize(document.PageSize);
            if (!string.IsNullOrEmpty(document.BackgroundColor))
                pageSize.BackgroundColor = new BaseColor(System.Drawing.Color.FromName(document.BackgroundColor));
            var margin = this.GetMargin(document.Margin);
            var itextDoc = new Document(pageSize, margin[0], margin[1], margin[2], margin[3]);
            if (!string.IsNullOrEmpty(document.Author))
                itextDoc.AddAuthor(document.Author);
            if (!string.IsNullOrEmpty(document.Subject))
                itextDoc.AddSubject(document.Subject);
            if (!string.IsNullOrEmpty(document.Keywords))
                itextDoc.AddKeywords(document.Keywords);
            if (!string.IsNullOrEmpty(document.Creator))
                itextDoc.AddCreator(document.Creator);
            if (document.Producer == "true")
                itextDoc.AddProducer();
            if (document.CreationDate == "true")
                itextDoc.AddCreationDate();
            PdfWriter.GetInstance(itextDoc, new MemoryStream());
            itextDoc.Open();
        }

        public void DrawingBody(PDFBody body)
        {
            foreach (var element in body.Elements)
            {

            }
        }

        private Rectangle GetPageSize(string pageSizeStr)
        {
            if (string.IsNullOrEmpty(pageSizeStr))
                return PageSize.A4;
            if (pageSizeStr.Contains(","))
            {
                var f = pageSizeStr.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                float f0, f1;
                if (f.Length != 2 || !float.TryParse(f[0], out f0) || !float.TryParse(f[1], out f1))
                    throw new FormatException("document.pageSize");
                return new Rectangle(f0, f1);
            }
            switch (pageSizeStr)
            {
                case "A4":
                    return PageSize.A4;
                default:
                    throw new ArgumentException("document.pageSize");
            }
        }
        private float[] GetMargin(string marginStr)
        {
            if (string.IsNullOrEmpty(marginStr))
                return new float[] { 36, 36, 36, 36 };
            if (marginStr.Contains(","))
            {
                var f = marginStr.Split(new char
                    [] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                var fr = new float[4];
                if (f.Length != 4 || !float.TryParse(f[0], out fr[0]) || !float.TryParse(f[1], out fr[1]) || !float.TryParse(f[2], out fr[2]) || !float.TryParse(f[3], out fr[3]))
                    throw new FormatException("document.margin");
                return fr;
            }
            float t;
            if (!float.TryParse(marginStr, out t))
                throw new FormatException("document.margin");
            return new float[] { t, t, t, t };
        }
    }
}

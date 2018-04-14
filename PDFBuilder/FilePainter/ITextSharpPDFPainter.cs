using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using FileBuilder.Models;

namespace FileBuilder
{
    internal class ITextSharpPDFPainter : IPDFPainter
    {
        protected Document _itextDocument;
        public void Drawing(FileContext context)
        {
            this.InitDocument(context);
            this._itextDocument.Open();
            this.DrawingBody(context);
            this._itextDocument.Close();
        }
        protected void DrawingBody(FileContext context)
        {
            foreach (var element in context.Document.Body.Elements)
            {
                if (element is FileP)
                {
                    var p = element as FileP;
                    if (!string.IsNullOrEmpty(p.Content))
                    {
                        this._itextDocument.Add(new Paragraph(p.Content));
                    }
                }
            }
        }

        protected void InitDocument(FileContext context)
        {
            var pageSize = this.GetPageSize(context.Document.PageSize);
            if (!string.IsNullOrEmpty(context.Document.BackgroundColor))
                pageSize.BackgroundColor = new BaseColor(System.Drawing.Color.FromName(context.Document.BackgroundColor));
            var margin = this.GetMargin(context.Document.Margin);
            this._itextDocument = new Document(pageSize, margin[0], margin[1], margin[2], margin[3]);
            if (!string.IsNullOrEmpty(context.Document.Author))
                this._itextDocument.AddAuthor(context.Document.Author);
            if (!string.IsNullOrEmpty(context.Document.Subject))
                this._itextDocument.AddSubject(context.Document.Subject);
            if (!string.IsNullOrEmpty(context.Document.Keywords))
                this._itextDocument.AddKeywords(context.Document.Keywords);
            if (!string.IsNullOrEmpty(context.Document.Creator))
                this._itextDocument.AddCreator(context.Document.Creator);
            if (context.Document.Producer == "true")
                this._itextDocument.AddProducer();
            if (context.Document.CreationDate == "true")
                this._itextDocument.AddCreationDate();
            context.File = new MemoryStream();
            PdfWriter.GetInstance(this._itextDocument, context.File);
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

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
    internal class iTextSharpPDFPainter : IPDFPainter
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
            RecursiveDrawingElement(context.Document.Body.Elements, null);
        }
        private void RecursiveDrawingElement(List<FileElement> elements, IElement parent)
        {
            foreach (var element in elements)
            {
                if (element is FileP)
                {
                    var p = element as FileP;
                    if (!string.IsNullOrEmpty(p.Content))
                    {
                        this._itextDocument.Add(new Paragraph(p.Content, this.GetFont(p.FontName, p.FontSize)));
                    }
                    else if (p.Childs.Count > 0)
                    {
                        var allowChildTypes = new List<Type>() { typeof(FileSpan), typeof(FileFont) };
                        var allowCount = p.Childs.Count(v => !allowChildTypes.Contains(v.GetType()));
                        if (allowCount > 0)
                            throw new ArgumentException("child not allow");
                        var paragraph = new Paragraph();
                        RecursiveDrawingElement(p.Childs, paragraph);
                    }
                }
                else if (element is FileFont)
                {
                    var font = element as FileFont;
                    if (!string.IsNullOrEmpty(font.Content))
                    {

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
        private Font GetFont(string fontName, string fontSize)
        {
            string fontPath;
            switch (fontName)
            {
                case "新宋体":
                    fontPath = @"C:\Windows\Fonts\simsun.ttc,1";
                    break;
                case "楷体":
                    fontPath = @"C:\Windows\Fonts\simkai.ttf";
                    break;
                case "黑体":
                    fontPath = @"C:\Windows\Fonts\simhei.ttf";
                    break;
                case "仿宋体":
                    fontPath = @"C:\Windows\Fonts\simfang.ttc";
                    break;
                default:
                    fontPath = @"C:\Windows\Fonts\simsun.ttc,0"; //宋体
                    break;
            }
            float size = float.TryParse(fontSize, out size) ? size : 20;
            return new Font(BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED), size);
        }
    }
}

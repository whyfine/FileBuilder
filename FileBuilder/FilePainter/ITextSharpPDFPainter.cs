using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using FileBuilder.Models;
using iTextSharp.text.pdf.draw;

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
        private void InitDocument(FileContext context)
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
        private void DrawingBody(FileContext context)
        {
            FileElementMappingToIElement(context.Document.Body.Elements, element => { this._itextDocument.Add(element); });
        }
        private void FileElementMappingToIElement(List<FileElement> elements, Action<IElement> addElement)
        {
            foreach (var element in elements)
            {
                IElement pdfElement = null;
                Action<IElement> addChildElement = null;
                if (element is FileTable)
                {
                    var table = element as FileTable;
                    if (table.Rows.Count.Equals(0))
                        continue;
                    pdfElement = this.CreatePdfPTable(table);
                    element.Childs.Clear();
                }
                else if (element is FileP)
                {
                    var paragraph = this.CreateParagraph(element as FileP);
                    pdfElement = paragraph;
                    addChildElement = childElement => { paragraph.Add(childElement); };
                }
                else if (element is FileSpan)
                {
                    var phrase = this.CreatePhrase(element as FileSpan);
                    pdfElement = phrase;
                    addChildElement = childElement => { phrase.Add(childElement); };
                }
                else if (element is FileFont)
                {
                    var chunk = this.CreateChunk(element as FileFont);
                    pdfElement = chunk;
                    element.Childs.Clear();
                }
                if (pdfElement != null)
                {
                    if (element.Childs.Count > 0)
                        FileElementMappingToIElement(element.Childs, addChildElement);
                    addElement(pdfElement);
                }
            }
        }

        private PdfPTable CreatePdfPTable(FileTable table)
        {
            var frow = table.Rows.First();
            var pdfPTable = new PdfPTable(table.Rows.Max(v => v.Cells.Count));
            this.SetPropertyFloat(table.Width, (width) => { pdfPTable.TotalWidth = width; });
            this.SetAlignment(table.Align,(align) => { pdfPTable.HorizontalAlignment = align; });
            
            foreach (var row in table.Rows)
            {
                var pdfPCells = new List<PdfPCell>();
                foreach (var cell in row.Cells)
                {
                    var pdfPCell = new PdfPCell(new Phrase(cell.Content, this.GetFont(cell.FontName, cell.FontSize, cell.Color, cell.FontStyle)));
                    if (cell.Childs.Count > 0)
                        FileElementMappingToIElement(cell.Childs, (element) => { pdfPCell.AddElement(element); });
                    pdfPCells.Add(pdfPCell);
                }
                var pdfPRow = new PdfPRow(pdfPCells.ToArray());
                pdfPTable.Rows.Add(pdfPRow);
            }
            return pdfPTable;
        }
        private Paragraph CreateParagraph(FileP p)
        {
            Paragraph paragraph;
            if (string.IsNullOrEmpty(p.Content))
            {
                paragraph = new Paragraph();
                paragraph.Font = this.GetFont(p.FontName, p.FontSize, p.Color, p.FontStyle);
            }
            else
                paragraph = new Paragraph(p.Content, this.GetFont(p.FontName, p.FontSize, p.Color, p.FontStyle));
            this.SetAlignment(p.Align, (align) => { paragraph.Alignment = align; });
            float f;
            this.SetPropertyFloat(p.Top, (top) => { paragraph.PaddingTop = top; });
            this.SetPropertyFloat(p.Left, (left) => { paragraph.IndentationLeft = left; });
            this.SetPropertyFloat(p.Right, (right) => { paragraph.IndentationRight = right; });
            this.SetPropertyFloat(p.Height, (height) => { paragraph.Leading = height; });
            this.SetPropertyFloat(p.Spacing, (spacing) => { paragraph.MultipliedLeading = spacing; });
            return paragraph;
            //else if (p.Childs.Count > 0)
            //{
            //    var allowChildTypes = new List<Type>() { typeof(FileSpan), typeof(FileFont) };
            //    var allowCount = p.Childs.Count(v => !allowChildTypes.Contains(v.GetType()));
            //    if (allowCount > 0)
            //        throw new ArgumentException("child not allow");
            //    var paragraph = new Paragraph();
            //    RecursiveDrawingElement(p.Childs, paragraph);
            //}
        }
        private Phrase CreatePhrase(FileSpan span)
        {
            Phrase phrase;
            if (string.IsNullOrEmpty(span.Content))
            {
                phrase = new Phrase();
                phrase.Font = this.GetFont(span.FontName, span.FontSize, span.Color, span.FontStyle);
            }
            else
                phrase = new Phrase(span.Content, this.GetFont(span.FontName, span.FontSize, span.Color, span.FontStyle));
            this.SetPropertyFloat(span.Height, (height) => { phrase.Leading = height; });
            return phrase;
        }
        private Chunk CreateChunk(FileFont font)
        {
            Chunk chunk;
            if (string.IsNullOrEmpty(font.Content))
            {
                chunk = new Chunk();
                chunk.Font = this.GetFont(font.FontName, font.FontSize, font.Color, font.FontStyle);
            }
            else
                chunk = new Chunk(font.Content, this.GetFont(font.FontName, font.FontSize, font.Color, font.FontStyle));
            float f;
            if (!string.IsNullOrEmpty(font.Height) && float.TryParse(font.Height, out f))
                chunk.setLineHeight(f);
            this.SetPropertyFloat(font.Height, (height) => { chunk.setLineHeight(height); });
            this.SetBaseColor(font.BackgroundColor,(color)=> { chunk.SetBackground(color); });
            return chunk;
        }

        protected Rectangle GetPageSize(string pageSizeStr)
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
        protected float[] GetMargin(string marginStr)
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
        protected Font GetFont(string fontName, string fontSize, string colorStr, string fontStyle)
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
            Font font = FontFactory.GetFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            if (!string.IsNullOrEmpty(fontStyle))
            {
                switch (fontStyle.ToLower())
                {
                    case "underline":
                        font.SetStyle(Font.UNDERLINE);
                        break;
                    default:
                        break;
                }
            }
            this.SetPropertyFloat(fontSize, (size) => { font.Size = size; });
            this.SetBaseColor(colorStr,(color)=> { font.Color = color; });
            return font;
        }
        protected void SetBaseColor(string color,Action<BaseColor> setMethod)
        {
            BaseColor c = null;
            if (!string.IsNullOrEmpty(color))
            {
                if (color.Contains(","))
                {
                    float[] rgb = new float[3];
                    var cArr = color.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    if (cArr.Length == 3 && float.TryParse(cArr[0], out rgb[0]) && float.TryParse(cArr[1], out rgb[1]) && float.TryParse(cArr[2], out rgb[2]))
                        c = new BaseColor(rgb[0], rgb[1], rgb[2]);
                }
                else
                {
                    switch (color)
                    {
                        case "black":
                            c = BaseColor.BLACK;
                            break;
                        case "red":
                            c = BaseColor.RED;
                            break;
                        default:
                            c = BaseColor.BLACK;
                            break;
                    }
                }
            }
            if (c != null && setMethod != null)
                setMethod(c);
        }
        protected void SetAlignment(string align, Action<int> setMethod)
        {
            if (string.IsNullOrEmpty(align))
                return;
            int i;
            switch (align)
            {
                case "center":
                    i = Rectangle.ALIGN_CENTER;
                    break;
                case "right":
                    i = Rectangle.ALIGN_RIGHT;
                    break;
                case "top":
                    i = Rectangle.ALIGN_TOP;
                    break;
                case "middle":
                    i = Rectangle.ALIGN_MIDDLE;
                    break;
                case "bottom":
                    i = Rectangle.ALIGN_BOTTOM;
                    break;
                case "left":
                default:
                    i = Rectangle.ALIGN_LEFT;
                    break;
            }
            if (setMethod != null)
                setMethod(i);
        }
        protected void SetPropertyFloat(string str, Action<float> setMethod)
        {
            float f;
            if (!string.IsNullOrEmpty(str) && float.TryParse(str, out f) && setMethod != null)
                setMethod(f);
        }
    }
}

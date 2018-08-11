using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TYFileBuilder.Painter;
using TYFileBuilder.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace TYFileBuilder.Adapter
{
    public class iTextSharpAdapter : IFileAdapter
    {
        protected Rectangle GetPageSize(FileElementStyle style)
        {
            if (!style.Width.HasValue || !style.Height.HasValue)
                return PageSize.A4;
            return new Rectangle(style.Width.Value, style.Height.Value);
        }
        protected void SetBackgroundColor()
        {

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
            this.SetBaseColor(colorStr, (color) => { font.Color = color; });
            return font;
        }
        protected void SetBaseColor(string color, Action<BaseColor> setMethod)
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

        Document IFileAdapter.DocumentMappingTo<Document>(FileDocument document)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TYFileBuilder.Models
{
    public class FileElementStyle
    {
        public string Color { get; set; }
        public string BackgroundColor { get; set; }

        public float? Height { get; set; }
        public float? Width { get; set; }

        public string FontName { get; set; }
        public string FontStyle { get; set; }
        public string FontSize { get; set; }

        public string Align { get; set; }
        public float? MarginTop { get; set; }
        public float? MarginLeft { get; set; }
        public float? MarginRight { get; set; }
    }
}

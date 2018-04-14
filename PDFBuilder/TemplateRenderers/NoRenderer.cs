using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFBuilder
{
    public class NoRenderer : IRenderer
    {
        private string _content;
        public NoRenderer(string content)
        {
            this._content = content;
        }
        public string Render()
        {
            return this._content;
        }
    }
}

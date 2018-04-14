using RazorEngine;
using RazorEngine.Templating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFBuilder
{
    public class RazorTemplateRenderer : IRenderer
    {
        private string _template;
        private object _data;
        public RazorTemplateRenderer(string template, object data)
        {
            this._template = template;
            this._data = data;
        }
        public string Render()
        {
            return
    Engine.Razor.RunCompile(this._template, "", null, this._data);
        }
    }
}

using PDFBuilder.Models;
using RazorEngine;
using RazorEngine.Templating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

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
        public PDFDocument Render()
        {
            var xml =
    Engine.Razor.RunCompile(this._template, "", null, this._data);
            var xmlSerializer = new XmlSerializer(typeof(PDFBuilder.Models.PDFDocument));
            return xmlSerializer.Deserializer<PDFBuilder.Models.PDFDocument>(xml);
        }
    }
}

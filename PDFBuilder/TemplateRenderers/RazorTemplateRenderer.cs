using FileBuilder.Models;
using RazorEngine;
using RazorEngine.Templating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FileBuilder
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
        public FileContext Render()
        {
            var context = new FileContext();
            var xml =
    Engine.Razor.RunCompile(this._template, "", null, this._data);
            var xmlSerializer = new XmlSerializer(typeof(FileBuilder.Models.FileDocument));
            context.Document= xmlSerializer.Deserializer<FileDocument>(xml);
            return context;
        }
    }
}

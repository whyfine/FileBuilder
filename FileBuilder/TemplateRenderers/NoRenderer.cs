using FileBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FileBuilder
{
    public class NoRenderer : IRenderer
    {
        private string _content;
        public NoRenderer(string content)
        {
            this._content = content;
        }
        public FileContext Render()
        {
            var context = new FileContext();
            var xmlSerializer = new XmlSerializer(typeof(FileDocument));
            context.Document = xmlSerializer.Deserializer<FileDocument>(this._content);
            return context;
        }
    }
}

using TYFileBuilder.Common;
using TYFileBuilder.Painter;
using TYFileBuilder.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TYFileBuilder
{
    public sealed class FileBuilder
    {
        private FileContext _context = new FileContext();
        private FilePainter _painter { get; set; }
        [ThreadStatic]
        private static FileBuilder _builder;
        private FileBuilder() { }
        public static FileBuilder GetInstance(FilePainter painter)
        {
            if (_builder == null)
            {
                _builder = new FileBuilder();
                _builder._painter = painter;
            }
            return _builder;
        }

        private FileDocument Parse()
        {
            var xmlSerializer = new XmlSerializer(typeof(FileDocument));
            return xmlSerializer.Deserializer<FileDocument>(this._context.XmlSource.ToString());
        }

        public async Task<MemoryStream> BuildToMemoryAsync(string xml, object data = null)
        {
            return await Task.Run(() =>
            {
                if (!xml.StartsWith("<"))
                {
                    if (!File.Exists(xml))
                        throw new FileNotFoundException("未找到xml文件");
                    xml = File.ReadAllText(xml);
                }
                if (data == null)
                    this._context.XmlSource = xml;
                else
                    this._context.XmlSource = RazorTemplateRenderer.Render(xml, data);
                this._context.Document = this.Parse();
                this._context.Stream = new MemoryStream();
                this._painter.Drawing(this._context);
                return this._context.Stream as MemoryStream;
            });
        }

        public async Task BuildToSaveAsync(string savePath, string xml, object data = null)
        {
            var stream = await this.BuildToMemoryAsync(xml, data);
            await Task.Run(() =>
             {
                 File.WriteAllBytes(savePath, stream.GetBuffer());
             });
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileBuilder
{
    public abstract class BaseFileBuilder : IFileBuilder
    {
        public byte[] Builder(IRenderer renderer)
        {
            throw new NotImplementedException();
        }

        public void Builder(string savePath, IRenderer renderer)
        {
            var context = renderer.Render();
            var filePainter = this.GetFilePainter();
            filePainter.Drawing(context);
            File.WriteAllBytes(savePath, context.File.GetBuffer());
        }

        public Task<byte[]> BuilderAsync(IRenderer renderer)
        {
            throw new NotImplementedException();
        }

        public Task BuilderAsync(string path, IRenderer renderer)
        {
            throw new NotImplementedException();
        }

        protected abstract IFilePainter GetFilePainter();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileBuilder
{
    public interface IFileBuilder
    {
        byte[] Builder(IRenderer renderer);
        void Builder(string savePath, IRenderer renderer);
        Task<byte[]> BuilderAsync(IRenderer renderer);
        Task BuilderAsync(string path, IRenderer renderer);

    }
}

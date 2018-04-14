using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFBuilder
{
    public interface IPDFBuilderAsync : IPDFBuilder
    {
        Task<byte[]> BuilderAsync(IRenderer renderer);
        Task BuilderAsync(string path, IRenderer renderer);
    }
}

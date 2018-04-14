using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFBuilder
{
    public interface IPDFBuilder
    {
        byte[] Builder(IRenderer renderer);
        void Builder(string savePath, IRenderer renderer);

    }
}

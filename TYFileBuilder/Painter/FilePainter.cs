using TYFileBuilder.Adapter;
using TYFileBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TYFileBuilder.Painter
{
    public abstract class FilePainter
    {
        public IFileAdapter _adapter { get; set; }
        public FilePainter(IFileAdapter adapter)
        {
            this._adapter = adapter;
        }
        public abstract void Drawing(FileContext context);
    }
}

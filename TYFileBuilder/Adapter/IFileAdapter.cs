using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TYFileBuilder.Models;

namespace TYFileBuilder.Adapter
{
    public interface IFileAdapter
    {
        T DocumentMappingTo<T>(FileDocument document) where T : class, new();
    }

}

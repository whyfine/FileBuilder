using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileBuilder.Models;

namespace FileBuilder
{
    public interface IRenderer
    {
        FileContext Render();
    }
}

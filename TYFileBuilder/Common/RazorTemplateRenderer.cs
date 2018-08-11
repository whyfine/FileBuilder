using TYFileBuilder.Models;
using RazorEngine;
using RazorEngine.Templating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TYFileBuilder.Common
{
    internal class RazorTemplateRenderer
    {
        internal static string Render(string template, object data)
        {
            return
    Engine.Razor.RunCompile(template, "", null, data);
        }
    }
}

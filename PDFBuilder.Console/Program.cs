using PDFBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GeneratePDF.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var xml = System.IO.File.ReadAllText(System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "2.xml"));
            var b = new XmlPDFBuilder();
            b.Builder(System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "1.pdf"), new NoRenderer(xml));
            Console.WriteLine("over");
            Console.ReadKey();
        }
    }
}

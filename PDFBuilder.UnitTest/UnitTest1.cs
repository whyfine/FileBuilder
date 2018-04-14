using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PDFBuilder.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var xml = System.IO.File.ReadAllText(System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "2.xml"));
            var b = new XmlPDFBuilder();
            b.Builder(System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "1.pdf"), new NoRenderer(xml));
        }
    }
}

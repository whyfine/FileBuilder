using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileBuilder.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var xml = System.IO.File.ReadAllText(System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "2.xml"));
            var b = new XmlFileBuilder();
            b.Builder(System.IO.Path.Combine(@"C:\Users\rick\Desktop", "1.pdf"), new RazorTemplateRenderer(xml, new { Content = "hello world 出来吧 神龙！" }));
            Assert.IsNotNull("");
        }
    }
}

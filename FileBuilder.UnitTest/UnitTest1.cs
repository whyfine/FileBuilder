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
            var xml = System.IO.File.ReadAllText(System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "1.xml"));
            var b = new XmlFileBuilder();
            b.Builder(System.IO.Path.Combine(@"C:\Users\rick\Desktop", Guid.NewGuid().ToString() + ".pdf"), new RazorTemplateRenderer(xml, new { P = "我是一个p",Span="我是一个span" }));
            Assert.IsNotNull("");
        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BizTalkComponents.PipelineComponents.WrapStringPipelineComponent;
using System.Text;
using System.IO;

namespace UnitTests
{
    [TestClass]
    public class UnwrapperStreamTest
    {
        [TestMethod]
        public void Read_WrappedStringUtf8_ProperOutput()
        {
            // arrange
            var encoding = Encoding.UTF8;
            var inStream = new MemoryStream(encoding.GetBytes("apabananAPA"));

            // act
            var target = new UnwrapperStream(inStream, "apa", "APA", encoding);
            var outStream = new MemoryStream();
            target.CopyTo(outStream);
            outStream.Position = 0;

            // assert
            var result = encoding.GetString(outStream.ToArray());
            Assert.AreEqual("banan", result);
        }

        [TestMethod]
        public void Read_WrappedStringAscii_ProperOutput()
        {
            // arrange
            var encoding = Encoding.ASCII;
            var inStream = new MemoryStream(encoding.GetBytes("apabananAPA"));

            // act
            var target = new UnwrapperStream(inStream, "apa", "APA", encoding);
            var outStream = new MemoryStream();
            target.CopyTo(outStream);
            outStream.Position = 0;

            // assert
            var result = encoding.GetString(outStream.ToArray());
            Assert.AreEqual("banan", result);
        }

        [TestMethod]
        public void Read_EmptyBody_ProperOutput()
        {
            // arrange
            var encoding = Encoding.ASCII;
            var inStream = new MemoryStream(encoding.GetBytes("apaAPA"));

            // act
            var target = new UnwrapperStream(inStream, "apa", "APA", encoding);
            var outStream = new MemoryStream();
            target.CopyTo(outStream);
            outStream.Position = 0;

            // assert
            var result = encoding.GetString(outStream.ToArray());
            Assert.AreEqual("", result);
        }
    }
}

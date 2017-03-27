using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Text;
using BizTalkComponents.PipelineComponents.WrapStringPipelineComponent;

namespace UnitTests
{
    [TestClass]
    public class WrapperStreamTest
    {
        [TestMethod]
        public void Read_Utf8WithBom_ProperOutput()
        {
            // arrange
            var encoding = new UTF8Encoding(true);
            var inStream = new MemoryStream(encoding.GetBytes("banan"));

            // act
            var target = new WrapperStream(inStream, "apa", "APA", encoding);
            var outStream = new MemoryStream();
            target.CopyTo(outStream);

            // assert
            var result = encoding.GetString(outStream.ToArray(), 0, (int)outStream.Length);
            Assert.AreEqual("apabananAPA", result);

        }

        [TestMethod]
        public void Read_Utf8NoBom_ProperOutput()
        {
            // arrange
            var encoding = new UTF8Encoding(false);
            var inStream = new MemoryStream(encoding.GetBytes("banan"));

            // act
            var target = new WrapperStream(inStream, "apa", "APA", encoding);
            var outStream = new MemoryStream();
            target.CopyTo(outStream);

            // assert
            var result = encoding.GetString(outStream.ToArray(), 0, (int)outStream.Length);
            Assert.AreEqual("apabananAPA", result);
        }

        [TestMethod]
        public void Read_ASCII_ProperOutput()
        {
            // arrange
            var encoding = Encoding.ASCII;
            var inStream = new MemoryStream(encoding.GetBytes("banan"));

            // act
            var target = new WrapperStream(inStream, "apa", "APA", encoding);
            var outStream = new MemoryStream();
            target.CopyTo(outStream);

            // assert
            var result = encoding.GetString(outStream.ToArray(), 0, (int)outStream.Length);
            Assert.AreEqual("apabananAPA", result);
        }

        [TestMethod]
        public void Length_KnownString_ProperLength()
        {
            // arrange
            var encoding = Encoding.UTF8;
            var inStream = new MemoryStream(encoding.GetBytes("banan"));

            // act
            var target = new WrapperStream(inStream, "apa", "APA", encoding);

            // assert
            Assert.AreEqual(encoding.GetByteCount("apabananAPA"), target.Length);
        }

        [TestMethod]
        public void Read_EmptyInStream_ProperOutput()
        {
            // arrange
            var encoding = new UTF8Encoding(false);
            var inStream = new MemoryStream();

            // act
            var target = new WrapperStream(inStream, "apa", "APA", encoding);
            var outStream = new MemoryStream();
            target.CopyTo(outStream);

            // assert
            var result = encoding.GetString(outStream.ToArray(), 0, (int)outStream.Length);
            Assert.AreEqual("apaAPA", result);
        }
    }
}

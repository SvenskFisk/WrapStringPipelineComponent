using BizTalkComponents.PipelineComponents.WrapStringPipelineComponent.PipelineComponents;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Text;
using Winterdom.BizTalk.PipelineTesting;

namespace UnitTests
{
    [TestClass]
    public class WrapStringDisassemblerTest
    {
        [TestMethod]
        public void Disassemble_NormalMessage_MessageUpdated()
        {
            // arrange
            var encoding = Encoding.UTF8;
            var pipeline = PipelineFactory.CreateEmptyReceivePipeline();
            pipeline.AddComponent(new WrapStringDisassembler { EncodingName = "utf-8" }, PipelineStage.Disassemble);

            var inStream = new MemoryStream(encoding.GetBytes("banan"));
            var inMessage = MessageHelper.CreateFromStream(inStream);

            // act
            var outMessage = pipeline.Execute(inMessage)[0];
            using (var sr = new StreamReader(outMessage.BodyPart.Data, encoding, false, 1024, true))
            {
                var result = sr.ReadToEnd();

                // assert
                Assert.AreEqual("<string><![CDATA[banan]]></string>", result);
            }
        }

        [TestMethod]
        public void Disassemble_EmptyMessage_MessageUpdated()
        {
            // arrange
            var encoding = Encoding.UTF8;
            var pipeline = PipelineFactory.CreateEmptyReceivePipeline();
            pipeline.AddComponent(new WrapStringDisassembler { EncodingName = "utf-8" }, PipelineStage.Disassemble);

            var inStream = new MemoryStream(encoding.GetBytes(""));
            var inMessage = MessageHelper.CreateFromStream(inStream);

            // act
            var outMessage = pipeline.Execute(inMessage)[0];
            using (var sr = new StreamReader(outMessage.BodyPart.Data, encoding, false, 1024, true))
            {
                var result = sr.ReadToEnd();

                // assert
                Assert.AreEqual("<string><![CDATA[]]></string>", result);
            }
        }
    }
}

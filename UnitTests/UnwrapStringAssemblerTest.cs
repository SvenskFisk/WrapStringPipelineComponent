using BizTalkComponents.PipelineComponents.WrapStringPipelineComponent.PipelineComponents;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Text;
using Winterdom.BizTalk.PipelineTesting;

namespace UnitTests
{
    [TestClass]
    public class UnwrapStringAssemblerTest
    {
        [TestMethod]
        public void Assemble_NormalMessage_MessageUpdated()
        {
            // arrange
            var encoding = Encoding.UTF8;
            var pipeline = PipelineFactory.CreateEmptySendPipeline();
            pipeline.AddComponent(new UnwrapStringAssembler { EncodingName = "utf-8" }, PipelineStage.Assemble);

            var inStream = new MemoryStream(encoding.GetBytes("<string><![CDATA[banan]]></string>"));
            var inMessage = MessageHelper.CreateFromStream(inStream);

            // act
            var outMessage = pipeline.Execute(new[] { inMessage });
            using (var sr = new StreamReader(outMessage.BodyPart.Data, encoding, false, 1024, true))
            {
                var result = sr.ReadToEnd();

                // assert
                Assert.AreEqual("banan", result);
            }
        }

        [TestMethod]
        public void Assemble_EmptyMessage_MessageUpdated()
        {
            // arrange
            var encoding = Encoding.UTF8;
            var pipeline = PipelineFactory.CreateEmptySendPipeline();
            pipeline.AddComponent(new UnwrapStringAssembler { EncodingName = "utf-8" }, PipelineStage.Assemble);

            var inStream = new MemoryStream(encoding.GetBytes("<string><![CDATA[]]></string>"));
            var inMessage = MessageHelper.CreateFromStream(inStream);

            // act
            var outMessage = pipeline.Execute(new[] { inMessage });
            using (var sr = new StreamReader(outMessage.BodyPart.Data, encoding, false, 1024, true))
            {
                var result = sr.ReadToEnd();

                // assert
                Assert.AreEqual("", result);
            }
        }
    }
}

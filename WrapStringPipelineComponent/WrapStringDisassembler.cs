namespace BizTalkComponents.PipelineComponents.WrapStringPipelineComponent.PipelineComponents
{
    using System;
    using System.Resources;
    using System.Drawing;
    using System.Collections;
    using System.Reflection;
    using System.ComponentModel;
    using System.Text;
    using System.IO;
    using Microsoft.BizTalk.Message.Interop;
    using Microsoft.BizTalk.Component.Interop;
    using System.Collections.Generic;
    using System.Xml.Linq;

    public partial class WrapStringDisassembler : IDisassemblerComponent
    {
        private static readonly Encoding utf8 = new UTF8Encoding(false);

        [DisplayName("Encoding")]
        public string EncodingName { get; set; }

        private Queue<IBaseMessage> _queue = new Queue<IBaseMessage>();

        public void Disassemble(IPipelineContext pContext, IBaseMessage pInMsg)
        {
            var inStream = pInMsg.BodyPart.GetOriginalDataStream();
            var encoding = Encoding.GetEncoding(string.IsNullOrWhiteSpace(EncodingName) ? "utf-8" : EncodingName);
            var outStream = new MemoryStream();

            using (var sr = new StreamReader(inStream, encoding, true, 1024 * 4, true))
            using (var sw = new StreamWriter(outStream, utf8, 1024 * 4, true))
            {
                var cdata = new XCData(sr.ReadToEnd());

                sw.Write("<string>");
                sw.Write(cdata.ToString());
                sw.Write("</string>");
            }

            outStream.Position = 0;
            pInMsg.BodyPart.Data = outStream;
            pContext.ResourceTracker.AddResource(outStream);
            pInMsg.Context.Promote("MessageType", "http://schemas.microsoft.com/BizTalk/2003/system-properties", "string");

            _queue.Enqueue(pInMsg);
        }

        public IBaseMessage GetNext(IPipelineContext pContext)
        {
            if (_queue.Count > 0)
            {
                return _queue.Dequeue();
            }

            return null;
        }
    }
}


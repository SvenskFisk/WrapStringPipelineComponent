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

    public partial class UnwrapStringAssembler : IAssemblerComponent
    {
        [DisplayName("Encoding")]
        public string EncodingName { get; set; }

        private Queue<IBaseMessage> _queue = new Queue<IBaseMessage>();

        public void AddDocument(IPipelineContext pContext, IBaseMessage pInMsg)
        {
            var inStream = pInMsg.BodyPart.GetOriginalDataStream();
            var encoding = Encoding.GetEncoding(pInMsg.BodyPart.Charset ?? "utf-8");
            var outStream = new UnwrapperStream(inStream, "<string><![CDATA[", "]]></string>", encoding);

            pInMsg.BodyPart.Data = outStream;
            pContext.ResourceTracker.AddResource(outStream);
            pInMsg.Context.Promote("MessageType", "http://schemas.microsoft.com/BizTalk/2003/system-properties", "string");

            _queue.Enqueue(pInMsg);
        }

        public IBaseMessage Assemble(IPipelineContext pContext)
        {
            if (_queue.Count > 0)
            {
                return _queue.Dequeue();
            }

            return null;
        }
    }
}


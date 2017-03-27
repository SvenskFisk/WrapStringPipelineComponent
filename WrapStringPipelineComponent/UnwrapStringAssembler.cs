namespace BizTalkComponents.PipelineComponents.WrapStringPipelineComponent.PipelineComponents
{
    using Microsoft.BizTalk.Component.Interop;
    using Microsoft.BizTalk.Message.Interop;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Text;
    using System.Xml;

    public partial class UnwrapStringAssembler : IAssemblerComponent
    {
        [DisplayName("Encoding")]
        public string EncodingName { get; set; }

        private Queue<IBaseMessage> _queue = new Queue<IBaseMessage>();

        public void AddDocument(IPipelineContext pContext, IBaseMessage pInMsg)
        {
            var inStream = pInMsg.BodyPart.GetOriginalDataStream();
            var encoding = Encoding.GetEncoding(string.IsNullOrWhiteSpace(EncodingName) ? "utf-8" : EncodingName);

            var doc = new XmlDocument();
            doc.Load(inStream);
            var outStream = new MemoryStream(encoding.GetBytes(doc.LastChild.InnerText));

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


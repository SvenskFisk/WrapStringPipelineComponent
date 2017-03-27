using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizTalkComponents.PipelineComponents.WrapStringPipelineComponent
{
    /// <summary>
    /// TODO Ugly implementation of unwrap. Change to streaming later.
    /// </summary>
    public sealed class UnwrapperStream : Stream
    {
        const int READ_SIZE = 1024 * 4;

        private int _headLength;

        private int _tailLength;

        private Stream _inStream;

        private Encoding _encoding;

        private MemoryStream _outStream;

        public UnwrapperStream(Stream inStream, string head, string tail, Encoding encoding)
        {
            _inStream = inStream;
            _encoding = encoding;

            _headLength = encoding.GetByteCount(head);
            _tailLength = encoding.GetByteCount(tail);

            UglyUnwrap();
        }

        public override bool CanRead
        {
            get { return true; }
        }

        public override bool CanSeek
        {
            get { return false; }
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override void Flush()
        {
            throw new NotSupportedException();
        }

        public override long Length
        {
            get { throw new NotSupportedException(); }
        }

        public override long Position { get; set; }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return _outStream.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException();
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }

        private void UglyUnwrap()
        {
            var buffer = new byte[_inStream.Length - _headLength - _tailLength];

            // read and discard the head
            var headBuffer = new byte[_headLength];
            var headOffset = 0;
            while(headOffset < _headLength)
            {
                headOffset += _inStream.Read(headBuffer, headOffset, Math.Min(READ_SIZE, _headLength - headOffset));
            }

            // read body until we reach tail
            var offset = 0;
            while (offset < _inStream.Length - _headLength - _tailLength)
            {
                var count = (int)Math.Min(READ_SIZE, _inStream.Length - _headLength - _tailLength - offset);
                offset += _inStream.Read(buffer, offset, count);
            }

            _outStream = new MemoryStream(buffer);
        }
    }
}

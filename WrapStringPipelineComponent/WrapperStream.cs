using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizTalkComponents.PipelineComponents.WrapStringPipelineComponent
{
    public sealed class WrapperStream : Stream
    {
        private enum WriteState { Head, Body, Tail };

        private WriteState _writeState;

        private int _offset;

        private byte[] _head;

        private byte[] _tail;

        private Stream _inStream;

        private Encoding _encoding;

        public WrapperStream(Stream inStream, string head, string tail, Encoding encoding)
        {
            _inStream = inStream;
            _encoding = encoding;

            _head = encoding.GetBytes(head ?? "");
            _tail = encoding.GetBytes(tail ?? "");
            _writeState = WriteState.Head;
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
            get { return _inStream.Length + _head.Length + _tail.Length; }
        }

        public override long Position
        {
            get { throw new NotSupportedException(); }
            set { throw new NotSupportedException(); }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (count == 0)
            {
                return 0;
            }

            switch (_writeState)
            {
                case WriteState.Head:
                    var headBytesToCopy = Math.Min(count, _head.Length - _offset);
                    Array.Copy(_head, _offset, buffer, offset, headBytesToCopy);
                    _offset += headBytesToCopy;

                    if (_offset >= _head.Length)
                    {
                        // count bigger than remaining _head.
                        _writeState = WrapperStream.WriteState.Body;
                        return headBytesToCopy + Read(buffer, offset + headBytesToCopy, count - headBytesToCopy);
                    }
                    else
                    {
                        // count <= _head.
                        return headBytesToCopy;
                    }

                case WriteState.Body:
                    var bytesRead = _inStream.Read(buffer, offset, count);
                    if (bytesRead == 0)
                    {
                        _writeState = WrapperStream.WriteState.Tail;
                        _offset = 0;
                        return Read(buffer, offset, count);
                    }
                    else
                    {
                        return bytesRead;
                    }

                case WriteState.Tail:
                    var tailBytesToCopy = Math.Min(count, _tail.Length - _offset);
                    Array.Copy(_tail, _offset, buffer, offset, tailBytesToCopy);
                    _offset += tailBytesToCopy;

                    return tailBytesToCopy;
                default:
                    throw new Exception("Illegal state");
            }
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
    }
}

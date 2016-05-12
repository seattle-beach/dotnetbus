using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace dotnetbus_web.Tests
{
    class FakeHttpContent : HttpContent
    {
        public string Content { get; set; }

        public FakeHttpContent(string content)
        {
            Content = content;
        }

        protected override bool TryComputeLength(out long length)
        {
            length = Content.Length;
            return true;
        }

        protected async override Task SerializeToStreamAsync(Stream stream, TransportContext context)
        {
            var buf = Encoding.UTF8.GetBytes(Content);
            await stream.WriteAsync(buf, 0, buf.Length);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace dotnetbus_web.Tests
{
    class FakeHttpResponseHandler : DelegatingHandler
    {
        public Uri lastUri { get; private set; }

        public Dictionary<Uri, HttpResponseMessage> responses { get; private set; }

        public FakeHttpResponseHandler()
        {
            responses = new Dictionary<Uri, HttpResponseMessage>();
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var url = request.RequestUri;

            if (!responses.ContainsKey(url))
            {
                throw new Exception(String.Format("No response configured for URL {0}", url));
            }

            var response = responses[url];
            var tcs = new TaskCompletionSource<HttpResponseMessage>();
            tcs.SetResult(response);
            return tcs.Task;
        }
    }
}

using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Community.Mvc.Extensions
{
    public static class HttpClientExtensions
    {
        //TODO: Paso 19 - 2 - Se implementa Patch 
        public static Task<HttpResponseMessage> PatchAsync(this HttpClient client, string requestUri,
            HttpContent content)
        {
            var method = new HttpMethod("PATCH");

            var request = new HttpRequestMessage(method, requestUri)
            {
                Content = content
            };

            return client.SendAsync(request);
        }

        public static Task<HttpResponseMessage> PatchAsync(this HttpClient client, Uri requestUri,
            HttpContent content)
        {
            var method = new HttpMethod("PATCH");

            var request = new HttpRequestMessage(method, requestUri)
            {
                Content = content
            };

            return client.SendAsync(request);
        }

        public static Task<HttpResponseMessage> PatchAsync(this HttpClient client, string requestUri,
            HttpContent content, CancellationToken cancellationToken)
        {
            var method = new HttpMethod("PATCH");

            var request = new HttpRequestMessage(method, requestUri)
            {
                Content = content
            };

            return client.SendAsync(request, cancellationToken);
        }

        public static Task<HttpResponseMessage> PatchAsync(this HttpClient client, Uri requestUri,
            HttpContent content, CancellationToken cancellationToken)
        {
            var method = new HttpMethod("PATCH");

            var request = new HttpRequestMessage(method, requestUri)
            {
                Content = content
            };

            return client.SendAsync(request, cancellationToken);
        }
    }
}
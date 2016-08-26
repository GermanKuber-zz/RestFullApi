using System;
using System.Net.Http;
using Community.Constants;

namespace Community.Mvc.Helpers
{
    public static class CustomHttpClient
    {

        public static HttpClient GetClient()
        {
            //TODO: Paso 13 - 2 - Cliente
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(CommunityConstants.ApiUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }
    }
}
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Community.Constants;

namespace Community.Mvc.Helpers
{
    public static class CustomHttpClient
    {

        public static HttpClient GetClient()
        {
         
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(CommunityConstants.ApiUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }
        public static HttpClient GetClient(string requestedVersion = null)
        {
            //TODO: Paso 20 - 1 - Se implementa WebClient - Versionado  
            HttpClient client = new HttpClient();



            client.BaseAddress = new Uri(Constants.CommunityConstants.ApiUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            if (requestedVersion != null)
            {
                
                client.DefaultRequestHeaders.Add("api-version", requestedVersion);

                


            }

            return client;
        }
    }

}
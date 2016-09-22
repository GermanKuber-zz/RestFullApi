using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Community.Constants;

namespace Community.Mvc.Helpers
{
    public static class ExpenseTrackerHttpClient
    {

        public static HttpClient GetClient(string requestedVersion = null)
        {
           
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(CommunityConstants.ApiUrl);

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            if (requestedVersion != null)
            {
                // through a custom request header
                //client.DefaultRequestHeaders.Add("api-version", requestedVersion);

                // through content negotiation
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/vnd.expensetrackerapi.v" 
                        + requestedVersion + "+json"));
            }


            return client;
        }
    }
}
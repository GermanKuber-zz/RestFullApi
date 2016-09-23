using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Web;
using Community.Constants;

namespace Community.Mvc.Helpers
{
    public static class CustomHttpClient
    {

        public static HttpClient GetClient()
        {
         
            HttpClient client = new HttpClient();
            //TODO: Paso 28 - 5 - Se agrega el access_token a el header del request
            //Install-Package Thinktecture.IdentityServer3.AccessTokenValidation
            var token = (HttpContext.Current.User.Identity as ClaimsIdentity).FindFirst("access_token");
            if (token != null)
            {
                client.SetBearerToken(token.Value);
            }
            client.BaseAddress = new Uri(CommunityConstants.ApiUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }
        public static HttpClient GetClient(string requestedVersion = null)
        {
          
            HttpClient client = new HttpClient();



            client.BaseAddress = new Uri(CommunityConstants.ApiUrl);
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
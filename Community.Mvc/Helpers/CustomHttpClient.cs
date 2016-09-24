using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Web;
using Community.Constants;
using Thinktecture.IdentityModel.Client;

namespace Community.Mvc.Helpers
{
    public static class CustomHttpClient
    {

        public static HttpClient GetClient()
        {
            //TODO : Paso 31 - 6
            CheckAndPossiblyRefreshToken((HttpContext.Current.User.Identity as ClaimsIdentity));

            HttpClient client = new HttpClient();
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
                client.DefaultRequestHeaders.Add("api-version", requestedVersion);


            return client;
        }
        //TODO : Paso 31 - 5
        private static async void CheckAndPossiblyRefreshToken(ClaimsIdentity id)
        {
            // check if the access token hasn't expired.
            if (DateTime.Now.ToLocalTime() >=
                 (DateTime.Parse(id.FindFirst("expires_at").Value)))
            {
                // expired.  Get a new one.
                var tokenEndpointClient = new OAuth2Client(
                    new Uri(CommunityConstants.IdSrvToken),
                    "mvc",
                    "secret");

                var tokenEndpointResponse =
                    await tokenEndpointClient
                    .RequestRefreshTokenAsync(id.FindFirst("refresh_token").Value);

                if (!tokenEndpointResponse.IsError)
                {
                    // replace the claims with the new values - this means creating a 
                    // new identity!                              
                    var result = from claim in id.Claims
                                 where claim.Type != "access_token" && claim.Type != "refresh_token" &&
                                       claim.Type != "expires_at"
                                 select claim;

                    var claims = result.ToList();

                    claims.Add(new Claim("access_token", tokenEndpointResponse.AccessToken));
                    claims.Add(new Claim("expires_at",
                                 DateTime.Now.AddSeconds(tokenEndpointResponse.ExpiresIn)
                                 .ToLocalTime().ToString()));
                    claims.Add(new Claim("refresh_token", tokenEndpointResponse.RefreshToken));

                    var newIdentity = new ClaimsIdentity(claims, "Cookies");
                    var wrapper = new HttpRequestWrapper(HttpContext.Current.Request);
                    wrapper.GetOwinContext().Authentication.SignIn(newIdentity);
                }
                else
                {
                    // log, ...
                    throw new Exception("An error has occurred");
                }
            }



        }
    }


}
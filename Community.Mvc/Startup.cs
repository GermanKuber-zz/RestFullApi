using Community.Constants;
using Community.Mvc.Helpers;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;

[assembly: OwinStartup(typeof(Community.Mvc.Startup))]

namespace Community.Mvc
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "Cookies"
            });

            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {
                ClientId = "mvc",
                Authority = CommunityConstants.IdSrv,
                RedirectUri = CommunityConstants.ClientUrl,
                SignInAsAuthenticationType = "Cookies",
                //TODO: Paso 24 - 1 - Agrego token
                ResponseType = "code id_token token",
                Scope = "openid profile",

                Notifications = new OpenIdConnectAuthenticationNotifications()
                {
                    MessageReceived = async n =>
                    {
                        EndpointAndTokenHelper.DecodeAndWrite(n.ProtocolMessage.IdToken);
                        EndpointAndTokenHelper.DecodeAndWrite(n.ProtocolMessage.AccessToken);


                        //TODO: Paso 24 - 2 - Consumo el end point
                        var userInfo = await EndpointAndTokenHelper.CallUserInfoEndpoint(n.ProtocolMessage.AccessToken);

                    }

                }
            });

        }



    }

}

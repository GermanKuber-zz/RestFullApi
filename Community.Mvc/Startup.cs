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
        //TODO: Paso 23 - 1 - Se activa ssl
        //Install-Package Microsoft.Owin.Security.Cookies
        //Install-Package Microsoft.Owin.Security.OpenIdConnect
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
                
                ResponseType = "code id_token",
                //TODO: Paso 23 - 3 - Se pide informacion del perfil
                Scope = "openid",
                //Scope = "openid profile",

                Notifications = new OpenIdConnectAuthenticationNotifications()
                {

                    MessageReceived = async n => EndpointAndTokenHelper.DecodeAndWrite(n.ProtocolMessage.IdToken)
                }

            });

        }



    }

}

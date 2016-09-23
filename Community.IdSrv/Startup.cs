using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Community.Constants;
using Community.IdSrv.Config;
using Microsoft.Owin;
using Microsoft.Owin.Security.Facebook;
using Newtonsoft.Json.Linq;
using Owin;
using Thinktecture.IdentityServer.Core.Configuration;

[assembly: OwinStartup(typeof(Community.IdSrv.Startup))]

namespace Community.IdSrv
{
    public class Startup
    {
        
        public void Configuration(IAppBuilder app)
        {
            app.UseWebApi(WebApiConfig.Register());
            app.Map("/identity", idsrvApp =>
            {
                idsrvApp.UseIdentityServer(new IdentityServerOptions
                {
                    SiteName = "Embedded IdentityServer",
                    IssuerUri = CommunityConstants.IdSrvIssuerUri,
                    SigningCertificate = LoadCertificate(),

                    Factory = InMemoryFactory.Create(
                        users: Users.Get(),
                        clients: Clients.Get(),
                        scopes: Scopes.Get()),
                    AuthenticationOptions = new AuthenticationOptions
                    {
                        IdentityProviders = ConfigureIdentityProviders
                    }
                });

            });
        }
        //TODO: Paso 27 - 2 - Configuro provider
        private void ConfigureIdentityProviders(IAppBuilder app, string signInAsType)
        {
            app.UseFacebookAuthentication(new FacebookAuthenticationOptions
            {
                AuthenticationType = "Facebook",
                Caption = "Sign-in with Facebook",
                SignInAsAuthenticationType = signInAsType,
                AppId = "1134802756604028",
                AppSecret = "470003f4b6ad7ded6dfc97f8b7b94910",
                Provider = new FacebookAuthenticationProvider()
                {
                    OnAuthenticated = (context) =>
                    {
                        //TODO: Paso 27 - 3 - Transformo los claims que recibo de facebook
                        JToken lastName, firstName;
                        if (context.User.TryGetValue("last_name", out lastName))
                        {
                            context.Identity.AddClaim(new System.Security.Claims.Claim(
                                Thinktecture.IdentityServer.Core.Constants.ClaimTypes.FamilyName,
                                lastName.ToString()));
                        }
                        else
                        {
                            context.Identity.AddClaim(new System.Security.Claims.Claim(
                                 Thinktecture.IdentityServer.Core.Constants.ClaimTypes.FamilyName,
                                 context?.Name?.Split(' ')[0].ToString()));

                        }

                        if (context.User.TryGetValue("first_name", out firstName))
                        {
                            context.Identity.AddClaim(new System.Security.Claims.Claim(
                                Thinktecture.IdentityServer.Core.Constants.ClaimTypes.GivenName,
                                firstName.ToString()));
                        }
                        else
                        {
                            context.Identity.AddClaim(new System.Security.Claims.Claim(
                                 Thinktecture.IdentityServer.Core.Constants.ClaimTypes.GivenName,
                                 context?.Name?.Split(' ')[1].ToString()));

                        }
                        context.Identity.AddClaim(new System.Security.Claims.Claim("role", "WebReadUser"));
                        context.Identity.AddClaim(new System.Security.Claims.Claim("role", "WebWriteUser"));

                        context.Identity.AddClaim(new System.Security.Claims.Claim("role", "MobileReadUser"));
                        context.Identity.AddClaim(new System.Security.Claims.Claim("role", "MobileWriteUser"));

                        return Task.FromResult(0);
                    }
                }


            });
        }

        //TODO: Paso 27 - 1 - Registro mi app en developers.facebook.com
        //Install-Package Microsoft.Owin.Security.Facebook
        //https://localhost:44344//identity//.well-known/openid-configuration
        //Cargamos los cerficiados
        //Habilitamos SSl
        //Cambiamos el puerto
        X509Certificate2 LoadCertificate()
        {
            return new X509Certificate2(
                $@"{AppDomain.CurrentDomain.BaseDirectory}\Certificate\idsrv3test.pfx", "idsrv3test");
        }
    }
}
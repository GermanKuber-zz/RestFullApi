using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Community.Constants;
using Community.IdSrv.Config;
using Microsoft.Owin;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;
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
            var opetions = new GoogleOAuth2AuthenticationOptions
            {
           Caption = "Sign-in with Google",
                SignInAsAuthenticationType = signInAsType,
                ClientId = "1085070976621-1g929v9ft3n9fhm9vb39sifg009muukl.apps.googleusercontent.com",
                ClientSecret = "nAo9Ga2gy1up8XTOQYRSl2B-",
                Provider = new GoogleOAuth2AuthenticationProvider()
                {
                    OnAuthenticated = (context) =>
                    {
                        JToken lastName, firstName;
                        if (context.User.TryGetValue("last_name", out lastName))
                        {
                            context.Identity.AddClaim(new System.Security.Claims.Claim(
                                Thinktecture.IdentityServer.Core.Constants.ClaimTypes.FamilyName,
                                lastName.ToString()));
                        }

                        if (context.User.TryGetValue("first_name", out firstName))
                        {
                            context.Identity.AddClaim(new System.Security.Claims.Claim(
                                Thinktecture.IdentityServer.Core.Constants.ClaimTypes.GivenName,
                                firstName.ToString()));
                        }
                        context.Identity.AddClaim(new System.Security.Claims.Claim("role", "WebReadUser"));
                        context.Identity.AddClaim(new System.Security.Claims.Claim("role", "WebWriteUser"));

                        context.Identity.AddClaim(new System.Security.Claims.Claim("role", "MobileReadUser"));
                        context.Identity.AddClaim(new System.Security.Claims.Claim("role", "MobileWriteUser"));

                        return Task.FromResult(0);
                    }
                }

            };

            app.UseGoogleAuthentication(opetions);
        }

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
using System;
using System.Security.Cryptography.X509Certificates;
using Community.Constants;
using Community.IdSrv.Config;
using Microsoft.Owin;
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
                        scopes: Scopes.Get())
                });
            });
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
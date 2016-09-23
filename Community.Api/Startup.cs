using Community.APi;
using Community.APi.Helper;
using Community.Constants;
using Microsoft.Owin;
using Owin;
using Thinktecture.IdentityServer.AccessTokenValidation;
[assembly: OwinStartup(typeof(Startup))]

namespace Community.APi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //TODO : 29 - 6

            app.UseResourceAuthorization(new AuthorizationManager());

            app.UseIdentityServerBearerTokenAuthentication(new
                 IdentityServerBearerTokenAuthenticationOptions
                    {
                        Authority = CommunityConstants.IdSrv,
                        RequiredScopes = new[] { "communityapi" }
                    });
            app.UseWebApi(WebApiConfig.Register());

        }
    }
}
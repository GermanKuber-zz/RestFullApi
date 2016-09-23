using System.Collections.Generic;
using Community.Constants;
using Thinktecture.IdentityServer.Core.Models;

namespace Community.IdSrv.Config
{
    public static class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new[]
             {
             new Client
                {
                     Enabled = true,
                     ClientId = "mvc",
                     ClientName = "Community MVC Client (Hybrid Flow)",
                     Flow = Flows.Hybrid,
                     RequireConsent = true,

                    RedirectUris = new List<string>
                    {
                        CommunityConstants.ClientUrl
                    }
                 },
                   new Client
                    {
                    ClientName = "Community Native Client (Implicit Flow)",
                    Enabled = true,
                    ClientId = "native",
                    Flow = Flows.Implicit,
                    RequireConsent = true,

                    ScopeRestrictions = new List<string>
                    {
                        Thinktecture.IdentityServer.Core.Constants.StandardScopes.OpenId,
                        "roles",
                        "communityapi"
                    },
                   },   new Client
                        {
                            Enabled = true,
                            ClientName = "Community MVC Client API Access (Client Credentials Flow)",
                            ClientId = "mvc_api",
                            ClientSecrets = new List<ClientSecret>() {new ClientSecret("secret".Sha256())},
                            Flow = Flows.ClientCredentials
                        }

             };

        }
    }
}
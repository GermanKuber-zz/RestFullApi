using System.Collections.Generic;
using Community.Constants;
using Thinktecture.IdentityServer.Core.Models;

namespace Community.APi.Config
{
    public static class Clients
    {
       
        //TODO: Paso 22 - 1 - Security Token - Implementar SSL
        public static IEnumerable<Client> Get()
        {
            return new[]
             {
                new Client
                {
                     Enabled = true,
                     ClientId = "mvc",
                     ClientName = "ExpenseTracker MVC Client (Hybrid Flow)",
                     Flow = Flows.Hybrid,
                     RequireConsent = true,

                    RedirectUris = new List<string>
                    {
                        CommunityConstants.ApiUrl
                    }
                 }
             };

        }
    }
}
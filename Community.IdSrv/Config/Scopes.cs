using System.Collections.Generic;
using Thinktecture.IdentityServer.Core.Models;

namespace Community.APi.Config
{
    public static class Scopes
    {
        //TODO: Paso 13 - 4 - Security Token 
        public static IEnumerable<Scope> Get()
        {
            var scopes = new List<Scope>
                {
 
                    // identity scopes

                    StandardScopes.OpenId,
                    StandardScopes.Profile 

                 };

            return scopes;
        }
 
    }
}
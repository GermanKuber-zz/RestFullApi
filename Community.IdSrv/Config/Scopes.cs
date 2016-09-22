using System.Collections.Generic;
using Thinktecture.IdentityServer.Core.Models;

namespace Community.IdSrv.Config
{
    public static class Scopes
    {
        public static IEnumerable<Scope> Get()
        {
            var scopes = new List<Scope>
                {
                    StandardScopes.OpenId,
                    StandardScopes.Profile 
                 };

            return scopes;
        }
 
    }
}
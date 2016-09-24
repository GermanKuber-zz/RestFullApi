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
                    StandardScopes.Profile,
                    new Scope
                    {
                        Enabled = true,
                        Name = "roles",
                        DisplayName = "Roles",
                        Description = "The roles you belong to.",
                        Type = ScopeType.Identity,
                        Claims = new List<ScopeClaim>
                        {
                            new ScopeClaim("role")
                        }
                    } ,new Scope
                    {

                        Name = "communityapi",
                        DisplayName = "Community API Scope",
                        Type = ScopeType.Resource,
                        Emphasize = false,
                        Enabled = true
                    },
                    //TODO : Paso 31 - 1 - Soporte fuera de linea
                      StandardScopes.OfflineAccess

                 };

            return scopes;
        }

    }
}
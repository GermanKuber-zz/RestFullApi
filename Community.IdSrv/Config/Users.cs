using System.Collections.Generic;
using System.Security.Claims;
using Thinktecture.IdentityServer.Core.Services.InMemory;

namespace Community.IdSrv.Config
{
    public static class Users
    {
        public static List<InMemoryUser> Get()
        {
            return new List<InMemoryUser>() {
            //TODO: Paso 26 - 1 - Agrego roles
               new InMemoryUser
            {
                Username = "German",
                Password = "secret",
                Subject = "1",

                Claims = new[]
                {
                    new Claim(Thinktecture.IdentityServer.Core.Constants.ClaimTypes.GivenName, "German"),
                    new Claim(Thinktecture.IdentityServer.Core.Constants.ClaimTypes.FamilyName, "German"),
                    new Claim(Thinktecture.IdentityServer.Core.Constants.ClaimTypes.Role, "WebReadUser"),
                      new Claim(Thinktecture.IdentityServer.Core.Constants.ClaimTypes.Role, "WebWriteUser"),
                      new Claim(Thinktecture.IdentityServer.Core.Constants.ClaimTypes.Role, "MobileReadUser"),
                      new Claim(Thinktecture.IdentityServer.Core.Constants.ClaimTypes.Role, "MobileWriteUser")
               }
            },
            new InMemoryUser
            {
                Username = "Federico",
                Password = "secret",
                Subject = "2",

                Claims = new[]
                {
                    new Claim(Thinktecture.IdentityServer.Core.Constants.ClaimTypes.GivenName, "Federico"),
                    new Claim(Thinktecture.IdentityServer.Core.Constants.ClaimTypes.FamilyName, "Federico"),
                      new Claim(Thinktecture.IdentityServer.Core.Constants.ClaimTypes.Role, "WebReadUser"),
                      new Claim(Thinktecture.IdentityServer.Core.Constants.ClaimTypes.Role, "MobileWriteUser")
               }
            },
            new InMemoryUser
            {
                Username = "Usuario",
                Password = "secret",
                Subject = "3",

                Claims = new[]
                {
                    new Claim(Thinktecture.IdentityServer.Core.Constants.ClaimTypes.GivenName, "Usuario"),
                    new Claim(Thinktecture.IdentityServer.Core.Constants.ClaimTypes.FamilyName, "Usuario"),
                      new Claim(Thinktecture.IdentityServer.Core.Constants.ClaimTypes.Role, "WebWriteUser"),
                      new Claim(Thinktecture.IdentityServer.Core.Constants.ClaimTypes.Role, "MobileWriteUser")
               }
            },
            new InMemoryUser
            {
                Username = "SuperUsuario",
                Password = "secret",
                Subject = "4",

                Claims = new[]
                {
                    new Claim(Thinktecture.IdentityServer.Core.Constants.ClaimTypes.GivenName, "SuperUsuario"),
                    new Claim(Thinktecture.IdentityServer.Core.Constants.ClaimTypes.FamilyName, "SuperUsuario"),
                      new Claim(Thinktecture.IdentityServer.Core.Constants.ClaimTypes.Role, "MobileReadUser"),
               }
            }};
        }
    }
}
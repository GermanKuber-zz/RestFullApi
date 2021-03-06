﻿using System.Collections.Generic;
using System.Security.Claims;
using Thinktecture.IdentityServer.Core;
using Thinktecture.IdentityServer.Core.Services.InMemory;

namespace Community.APi.Config
{
    public static class Users
    {
        //TODO: Paso 13 - 5 - Security Token 
        public static List<InMemoryUser> Get()
        {
            return new List<InMemoryUser>() {
           
               new InMemoryUser
            {
                Username = "German",
                Password = "secret",
                Subject = "1",

                Claims = new[]
                {
                    new Claim(Thinktecture.IdentityServer.Core.Constants.ClaimTypes.GivenName, "German"),
                    new Claim(Thinktecture.IdentityServer.Core.Constants.ClaimTypes.FamilyName, "German"),
               }
            }
            ,
            new InMemoryUser
            {
                Username = "Federico",
                Password = "secret",
                Subject = "2",

                Claims = new[]
                {
                    new Claim(Thinktecture.IdentityServer.Core.Constants.ClaimTypes.GivenName, "Federico"),
                    new Claim(Thinktecture.IdentityServer.Core.Constants.ClaimTypes.FamilyName, "Federico"),
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
               }
            }

           };
        }
    }
}
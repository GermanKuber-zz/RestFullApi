using System.Net.Http.Headers;
using System.Web.Http;
using CacheCow.Server;
using Community.APi.IOC;
using Community.Core.Interfaces.Context;
using Community.Core.Interfaces.Repositorys;
using Community.Core.Interfaces.Services;
using Community.Repository;
using Community.Repository.Context;
using Community.Service;
using Microsoft.Practices.Unity;
using Newtonsoft.Json.Serialization;
using Swashbuckle.Application;

namespace Community.APi
{
    public static class WebApiConfig
    {
        public static HttpConfiguration Register()
        {
            var config = new HttpConfiguration();
           
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(name: "DefaultRouting",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });


           
            config.MessageHandlers.Add(new CachingHandler(config));
            return config;
            //TODO: Paso 13 - 2 - Security Token - Install-Package Microsoft.Owin.Host.SystemWeb
            //Install-Package Thinktecture.IdentityServer3

        }
    }
}

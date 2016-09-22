using System.Web.Http;
using CacheCow.Server;

namespace Community.IdSrv
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
        }
    }
}

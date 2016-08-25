using System.Net.Http.Headers;
using System.Web.Http;
using Community.APi.IOC;
using Community.Core.Interfaces.Context;
using Community.Core.Interfaces.Repositorys;
using Community.Core.Interfaces.Services;
using Community.Repository;
using Community.Repository.Context;
using Community.Service;
using Microsoft.Practices.Unity;
using Swashbuckle.Application;

namespace Community.APi
{
    public static class WebApiConfig
    {
        public static HttpConfiguration Register()
        {
            var config = new HttpConfiguration();
            config.EnableSwagger(c => { c.SingleApiVersion("v1", "Community - Api"); }).EnableSwaggerUi();

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(name: "DefaultRouting",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });


            config.Formatters.XmlFormatter.SupportedMediaTypes.Clear();

            config.Formatters.JsonFormatter.SerializerSettings.Formatting
                = Newtonsoft.Json.Formatting.Indented;
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver
                = new CamelCasePropertyNamesContractResolver();

            //TODO: Paso 5 - 3 - Se Implementa Patch
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(
          new MediaTypeHeaderValue("application/json-patch+json"));

            var container = new UnityContainer();
            container.RegisterType<IUserRepository, UserRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ICommunityRepository, CommunityRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ICommunityTagRepository, CommunityTagRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IUserService, UserService>(new HierarchicalLifetimeManager());
            container.RegisterType<ICommunityTagService, CommunityTagService>(new HierarchicalLifetimeManager());
            container.RegisterType<ICommunityService, CommunityService>(new HierarchicalLifetimeManager());
            container.RegisterType<ICommunityContext, CommunityContext>(new HierarchicalLifetimeManager());

            config.DependencyResolver = new UnityResolver(container);

            return config;

        }
    }
}

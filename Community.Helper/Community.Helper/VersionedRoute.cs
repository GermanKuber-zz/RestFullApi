using System.Collections.Generic;
using System.Web.Http.Routing;

namespace Community.Helper
{
    public class VersionedRoute : RouteFactoryAttribute
    {
        //TODO: Paso 12 - 4 - Versionado de Api 
        public VersionedRoute(string template, int allowedVersion)
            : base(template)
        {
            AllowedVersion = allowedVersion;
        }

        public int AllowedVersion
        {
            get;
            private set;
        }

        public override IDictionary<string, object> Constraints
        {
            get
            {
                var constraints = new HttpRouteValueDictionary();
                constraints.Add("version", new VersionConstraint(AllowedVersion));
                return constraints;
            }
        }
    }
}
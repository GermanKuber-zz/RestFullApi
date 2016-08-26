using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http.Routing;

namespace Community.Helper
{
    public class VersionConstraint : IHttpRouteConstraint
    {
        public const string VersionHeaderName = "api-version";

        private const int DefaultVersion = 1;

        public VersionConstraint(int allowedVersion)
        {
            AllowedVersion = allowedVersion;
        }

        public int AllowedVersion
        {
            get;
            private set;
        }

        public bool Match(HttpRequestMessage request, IHttpRoute route,
            string parameterName, IDictionary<string, object> values, HttpRouteDirection routeDirection)
        {
      
            if (routeDirection == HttpRouteDirection.UriResolution)
            {
                
                int? version = GetVersionFromCustomRequestHeader(request);
                
                if (version == null)
                    version = GetVersionFromCustomContentType(request);

                return ((version ?? DefaultVersion) == AllowedVersion);
            }
            return true;
        }

        private int? GetVersionFromCustomContentType(HttpRequestMessage request)
        {
            string versionAsString = null;
       

            var mediaTypes = request.Headers.Accept.Select(h => h.MediaType);
            string matchingMediaType = null;

            Regex regEx = new Regex(@"application\/vnd\.communityapi\.v([\d]+)\+json");

            //Verificamos si existe alguno concuerda con la Regex
            foreach (var mediaType in mediaTypes)
            {
                if (regEx.IsMatch(mediaType))
                {
                    matchingMediaType = mediaType;
                    break;
                }
            }

            if (matchingMediaType == null)
                return null;

            // Extraemos la version
            Match m = regEx.Match(matchingMediaType);
            versionAsString = m.Groups[1].Value;


            int version;
            if (versionAsString != null && Int32.TryParse(versionAsString, out version))
                return version;

            return null;
        }

        private int? GetVersionFromCustomRequestHeader(HttpRequestMessage request)
        {
           
            string versionAsString;
            IEnumerable<string> headerValues;
            if (request.Headers.TryGetValues(VersionHeaderName, out headerValues) && headerValues.Count() == 1)
            {
                versionAsString = headerValues.First();
            }
            else
            {
                return null;
            }

            int version;
            if (versionAsString != null && Int32.TryParse(versionAsString, out version))
            {
                return version;
            }

            return null;
        }
    }
}
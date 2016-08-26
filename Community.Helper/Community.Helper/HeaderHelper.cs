using System.Linq;
using System.Net.Http.Headers;
using Community.ViewModel;
using Newtonsoft.Json;

namespace Community.Helper
{
    public static class HeaderHelper
    {

        public static PagingInfoViewModel FindAndParsePagingInfo(HttpResponseHeaders responseHeaders)
        {
            //TODO: Paso 17 - 1 - Paginacion
            if (responseHeaders.Contains("X-Pagination"))
            {
                var xPag = responseHeaders.First(ph => ph.Key == "X-Pagination").Value;

           
                return JsonConvert.DeserializeObject<PagingInfoViewModel>(xPag.First());
            }


            return null;
        }
    }
}
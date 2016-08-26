using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Community.Mvc.Helpers;
using Community.ViewModel.Request;
using Newtonsoft.Json;

namespace Community.Mvc.Controllers
{
    public class UsersController : Controller
    {

        // GET: Users
        public async Task<ActionResult> Index()
        {
            //TODO: Paso 13 - 3 - Cliente
            var client = CustomHttpClient.GetClient();


            var response = await client.GetAsync("api/users");


            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<IEnumerable<UserViewModel>>(content);
                return View(list);

            }
            return Content("An error occurred.");




        }
    }
}
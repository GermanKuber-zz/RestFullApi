using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
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
        public ActionResult Create()
        {
            return View();
        }

        // POST: ExpenseGroups/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(UserViewModel model)
        {
            try
            {
                //TODO: Paso 14 - 1 - 
                var client = CustomHttpClient.GetClient();

                var serializedItemToCreate = JsonConvert.SerializeObject(model);

                var response = await client.PostAsync("api/users",
                    new StringContent(serializedItemToCreate,
                        System.Text.Encoding.Unicode,
                        "application/json"));

                if (response.IsSuccessStatusCode)
                    return RedirectToAction("Index");
                else
                    return Content("An error occurred.");
            }
            catch
            {
                return Content("An error occurred.");
            }

        }
    }
}
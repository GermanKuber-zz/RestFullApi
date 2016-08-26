using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Community.Mvc.Helpers;
using Community.ViewModel.Request;
using Newtonsoft.Json;

namespace Community.Mvc.Controllers
{
    public class CommunitysController : Controller
    {

        // GET: Users
        public async Task<ActionResult> Index()
        {

            var client = CustomHttpClient.GetClient();


            var response = await client.GetAsync("api/communitys");


            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<IEnumerable<CommunityViewModel>>(content);
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
        public async Task<ActionResult> Create(CommunityViewModel model)
        {
            try
            {
                var client = CustomHttpClient.GetClient();

                var serializedItemToCreate = JsonConvert.SerializeObject(model);

                var response = await client.PostAsync("api/communitys",
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
        // GET: ExpenseGroups/Edit/5

        public async Task<ActionResult> Edit(int id)
        {

            var client = CustomHttpClient.GetClient();

            HttpResponseMessage response = await client.GetAsync("api/communitys/" + id);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<CommunityViewModel>(content);
                return View(model);
            }

            return Content("An error occurred.");

        }

        // POST: ExpenseGroups/Edit/5   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, CommunityViewModel model)
        {
            try
            {
                var client = CustomHttpClient.GetClient();

                // serialize & PUT
                var serializedItemToUpdate = JsonConvert.SerializeObject(model);

                var response = await client.PutAsync("api/communitys/" + id,
                    new StringContent(serializedItemToUpdate,
                        System.Text.Encoding.Unicode, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return Content("An error occurred");
                }

            }
            catch
            {
                return Content("An error occurred");
            }
        }

        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var client = CustomHttpClient.GetClient();

                var response = await client.DeleteAsync("api/communitys/" + id);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return Content("An error occurred");
                }

            }
            catch
            {
                return Content("An error occurred");
            }
        }
    }
}
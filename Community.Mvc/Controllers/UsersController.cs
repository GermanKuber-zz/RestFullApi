using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Community.Helper;
using Community.Mvc.Helpers;
using Community.ViewModel.Request;
using Newtonsoft.Json;
using PagedList;

namespace Community.Mvc.Controllers
{
    public class UsersController : Controller
    {

        // GET: Users
        public async Task<ActionResult> Index(int? page = 1)
        {


            var client = CustomHttpClient.GetClient();


            HttpResponseMessage response =
                await client.GetAsync("api/users?sort=name&page=" + page + "&pagesize=5");

            var returnValue = new UserReturnViewModel();
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();

                var pagingInfo = HeaderHelper.FindAndParsePagingInfo(response.Headers);

                var list = JsonConvert.DeserializeObject<IEnumerable<UserViewModel>>(content);
           
                var pagedList = new StaticPagedList<UserViewModel>(list,
                    pagingInfo.CurrentPage,
                    pagingInfo.PageSize, pagingInfo.TotalCount);
                returnValue.PagingInfo = pagingInfo;
                returnValue.Users = pagedList;
                return View(returnValue);

            }
            else
            {
                return Content("An error occurred.");
            }

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
        // GET: ExpenseGroups/Edit/5

        public async Task<ActionResult> Edit(int id)
        {

            var client = CustomHttpClient.GetClient();

            HttpResponseMessage response = await client.GetAsync("api/users/" + id);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<UserViewModel>(content);
                return View(model);
            }

            return Content("An error occurred.");

        }

        // POST: ExpenseGroups/Edit/5   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, UserViewModel model)
        {
            try
            {
                var client = CustomHttpClient.GetClient();

                // serialize & PUT
                var serializedItemToUpdate = JsonConvert.SerializeObject(model);

                var response = await client.PutAsync("api/users/" + id,
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

                var response = await client.DeleteAsync("api/users/" + id);

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


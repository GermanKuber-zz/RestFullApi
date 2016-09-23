using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;
using Community.Helper;
using Community.Mvc.Extensions;
using Community.Mvc.Helpers;
using Community.ViewModel.Request;
using Marvin.JsonPatch;
using Newtonsoft.Json;
using PagedList;
using Thinktecture.IdentityModel.Mvc;

namespace Community.Mvc.Controllers
{

    public class CommunitysController : Controller
    {

        //Probamos con usuario Federico

        [ResourceAuthorize("Read", "Communitys")]
        public async Task<ActionResult> Index(int? page = 1)
        {


            var claimsIdentity = User.Identity as ClaimsIdentity;
            var userId = claimsIdentity.FindFirst("unique_user_key").Value;

            var client = CustomHttpClient.GetClient();



            HttpResponseMessage response =
                await client.GetAsync("api/communitys?sort=name&page=" + page + "&pagesize=5");

            var returnValue = new CommunityReturnViewModel();
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();

                var pagingInfo = HeaderHelper.FindAndParsePagingInfo(response.Headers);

                var list = JsonConvert.DeserializeObject<IEnumerable<CommunityViewModel>>(content);


                var pagedList = new StaticPagedList<CommunityViewModel>(list,
                    pagingInfo.CurrentPage,
                    pagingInfo.PageSize, pagingInfo.TotalCount);
                returnValue.PagingInfo = pagingInfo;
                returnValue.Communitys = pagedList;
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

        [ResourceAuthorize("Write", "Communitys")]
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

        [ResourceAuthorize("Write", "Communitys")]
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

        [ResourceAuthorize("Write", "Communitys")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, CommunityViewModel model)
        {
            try
            {
                var client = CustomHttpClient.GetClient();

           
                JsonPatchDocument<CommunityViewModel> doc = new JsonPatchDocument<CommunityViewModel>();
                doc.Replace(eg => eg.Description, model.Description);
             
                //doc.Replace(eg => eg.Name, model.Name);

                var requestObj = new PatchCommunityViewModel
                {
                    Id = id,
                    Model = doc
                };

                var serializedItemToUpdate = JsonConvert.SerializeObject(requestObj);
                var response = await client.PatchAsync("api/communitys/" + id,
                              new StringContent(serializedItemToUpdate, System.Text.Encoding.Unicode, "application/json"));



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

        
        public async Task<ActionResult> Details(int id)
        {
            var client = CustomHttpClient.GetClient();

            HttpResponseMessage response = await client.GetAsync("api/communitys/" + id
            + "?fields=id,description,name,tags");

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<CommunityViewModel>(content);
                return View(model);
            }

            return Content("An error occurred");
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
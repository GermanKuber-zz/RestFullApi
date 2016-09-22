using System.Web;
using System.Web.Mvc;

namespace Community.Mvc.Controllers
{
    public class HeaderController : Controller
    {
        public ActionResult Logout()
        {
            Request.GetOwinContext().Authentication.SignOut();
            return Redirect("/");
        }
    }
}
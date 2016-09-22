using System.Web;
using System.Web.Mvc;

namespace Community.Mvc.Controllers
{
    public class HeaderController : Controller
    {
        //TODO: Paso 23 - 4 - Logout
        public ActionResult Logout()
        {
            Request.GetOwinContext().Authentication.SignOut();
            return Redirect("/");
        }
    }
}
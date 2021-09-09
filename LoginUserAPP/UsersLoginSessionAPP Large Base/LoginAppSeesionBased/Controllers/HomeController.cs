using System.Web.Mvc;

namespace LoginAppSeesionBased.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if(Session["User"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Auth");
            }
            
        }
    }
}
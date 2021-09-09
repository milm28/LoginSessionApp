
using LoginAppSessionBased.Models.EFModels;
using System.Collections.Generic;
using System.Web.Mvc;

namespace LoginAppSessionBased.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if(Session["User"] != null)
            {
                List<tblUserr> users = new List<tblUserr>();
                for (int i = 0; i < 100000; i++)
                {
                    users.Add(new tblUserr
                    {
                     
                        Name = "Name" + i.ToString(),
                        UserName = "UserName" + i.ToString()


                    });
                }
                using (LoginAppUsingSessionDBEntities db = new LoginAppUsingSessionDBEntities())
                {
                    db.tblUserrs.AddRange(users);
                    db.SaveChanges();
                }
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Auth");
            }
            
        }

    }
}
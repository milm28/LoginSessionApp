using LoginAppSessionBased.DTOs;
using LoginAppSessionBased.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoginAppSessionBased.Controllers
{
    public class AuthController : Controller
    {
        #region Login
        public ActionResult LogIn()
        {

            return View(new LoginDTO());
        }

        [HttpPost]
        public ActionResult Login(LoginDTO loginDTO)
        {
            if (ModelState.IsValid)
            {
                tblUserr userDB;

                
                using (LoginAppUsingSessionDBEntities db = new LoginAppUsingSessionDBEntities())
                {
                    userDB = db.tblUserrs.Where(x => x.UserName.Equals(loginDTO.UserName)).FirstOrDefault();
                  

                    //koristimo snimanje u bazu za primer ucitavanja u tabelu vise podataka
                    //odnosi se za for petlju ovo snimanje promena
                    //db.tblUsers.AddRange(users);
                    //db.SaveChanges();
                }
                if(userDB == null)
                {
                    ModelState.AddModelError("UserName", "Kosrisnik sa imenom koje ste uneli ne ostoji u Bazi");
                    return View(loginDTO);
                }
                else
                {
                    if (userDB.Password.Equals(loginDTO.Password))
                    {
                        Session["User"] = userDB;
                        
                        return RedirectToAction("Index", "Home");

                    }
                    else
                    {
                        // korisnicka sifra nije validna
                        ModelState.AddModelError("Password", "Sifra nije ispravna");
                        return View(loginDTO);
                    }
                }
              
            }
            else
            {
                return View(loginDTO);
            }
            
        }
        #endregion

        #region logOut
        public ActionResult LogOut()
        {
            Session["User"] = null;
            return RedirectToAction("Login", "Auth");
        }
        #endregion
    }
}
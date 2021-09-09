using LoginAppSeesionBased.DTOs;
using LoginAppSeesionBased.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoginAppSeesionBased.Controllers
{
    public class AuthController : Controller
    {
        #region LogIn
        [HttpGet]
        public ActionResult Login()
        {
            //List<tblUser> users = new List<tblUser>();
            //byte isActive;
            //for (int i = 0; i < 15000; i++)
            //{
            //    if(i % 2 == 0)
            //    {
            //        isActive = 1;
            //    }
            //    else
            //    {
            //        isActive = 0;
            //    }
            //    users.Add(new tblUser
            //    {
            //        IsActive = isActive,
            //        Name = "Name_" + i.ToString(),
            //        Surname = "Surname_" + i.ToString(),
            //        Username = "Username_" + i.ToString(),
            //        Password = "Password_" + i.ToString(),
            //    });
            //}
            //using (LoginAppUsingSessionsDBEntities db = new LoginAppUsingSessionsDBEntities())
            //{
            //    db.tblUsers.AddRange(users);
            //    db.SaveChanges();
            //}
            return View(new LoginDTO());
        }
        [HttpPost]
        public ActionResult Login(LoginDTO loginDTO)
        {
            if (ModelState.IsValid)
            {
                /*
                 * Uneti podaci su validni. 
                 */
                tblUser userDB;
                // Proveri da li postoji korisnik u bazi sa unetim email-om
                using (LoginAppUsingSessionsDBEntities db = new LoginAppUsingSessionsDBEntities())
                {
                    userDB = db.tblUsers.Where(x => x.Username.Equals(loginDTO.Username)).FirstOrDefault();
                }

                if(userDB == null)
                {
                    // Korisnik sa prosledjenim emailom ne postoji u bazi podataka
                    ModelState.AddModelError("Username", "Korisnicko ime koje ste uneli ne postoji u bazi podataka.");
                    return View(loginDTO);
                }
                else
                {
                    // korisnik sa unetim emailom postoji u bazi
                    if (userDB.Password.Equals(loginDTO.Password))
                    {
                        // Korisnik postoji i uneo je dobru sifru, treba da ga 
                        // "Ulogujemo"
                        /*
                         * Posto koristimo Session, moramo "to nesto" da ubacimo
                         * u sesiju. Sada sledi smestanje objekta tblUser u sesiju
                         */
                        Session["User"] = userDB;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        // Korisnicke sifre se razlikuju
                        ModelState.AddModelError("Password", "Lozinka nije tacna, pokusajte ponovo.");
                        return View(loginDTO);
                    }
                }
            }
            else
            {
                /*
                 * Uneti podaci nisu validni, vrati nas na View sa unetim podacima.
                 */
                return View(loginDTO);
            }
            
        }

        #endregion
        #region LogOut

        public ActionResult LogOut()
        {
            Session["User"] = null;
            return RedirectToAction("Index", "Home");
        }

        #endregion
    }
}
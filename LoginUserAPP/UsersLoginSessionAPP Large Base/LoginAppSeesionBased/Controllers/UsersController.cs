using LoginAppSeesionBased.DTOs;
using LoginAppSeesionBased.Models.EFModels;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace LoginAppSeesionBased.Controllers
{
    public class UsersController : Controller
    {
        LoginAppUsingSessionsDBEntities _db;
        
        public UsersController()
        {
            _db = new LoginAppUsingSessionsDBEntities();
        }

        // GET: Users
        public ActionResult Index(int rwaId = 0)
        {
            return View();
        }
        /// <summary>
        /// JSON metoda koja vraca podatke za DataTable
        /// </summary>
        /// <param name="sEcho">Predstavlja eho tj koliko puta renderujemo tablu</param>
        /// <param name="iDisplayStart">Od koje kolone pocinjemo</param>
        /// <param name="iDisplayLength">Koliko podataka po strani prikazujemo</param>
        /// <param name="sSearch">Search term</param>
        /// <returns></returns>
        public string GetUserList(string sEcho, int iDisplayStart, 
            int iDisplayLength, string sSearch)
        {
            // Da bi pretraga bila valjana moramo sve da pretrazujemo kao mala ili kao velika slova
            sSearch = sSearch.ToLower();
            // Broj svih usera u tabeli
            int totalRecords = _db.tblUsers.Count();
            List<UsersListDTO> users;

            if (string.IsNullOrEmpty(sSearch))
            {
                users = _db.tblUsers.Select(x => new UsersListDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    Surname = x.Surname,
                    Username = x.Username,
                    IsActive = x.IsActive == 1 ? "Active" : "Inactive"
                })
                .OrderBy(x => x.Id)
                .Skip(iDisplayStart)
                .Take(iDisplayLength)
                .ToList();
            }
            else
            {
                users = users = _db.tblUsers.Where(x => x.Name.ToLower().Contains(sSearch)
                || x.Surname.ToLower().Contains(sSearch) || x.Username.ToLower().Contains(sSearch))
                    .Select(x => new UsersListDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    Surname = x.Surname,
                    Username = x.Username,
                    IsActive = x.IsActive == 1 ? "Active" : "Inactive"
                })
                .OrderBy(x => x.Id)
                .Skip(iDisplayStart)
                .Take(iDisplayLength)
                .ToList();
            }
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Clear();
            stringBuilder.Append("{");
            stringBuilder.Append("\"sEcho\" : ");
            stringBuilder.Append(sEcho);
            stringBuilder.Append(",");
            stringBuilder.Append("\"iTotalRecords\" : ");
            stringBuilder.Append(totalRecords);
            stringBuilder.Append(",");
            stringBuilder.Append("\"iTotalDisplayRecords\" : ");
            if (string.IsNullOrEmpty(sSearch))
            {
                stringBuilder.Append(totalRecords);
            }
            else
            {
                stringBuilder.Append(users.Count);
            }
                
            stringBuilder.Append(",");
            stringBuilder.Append("\"aaData\" : ");
            stringBuilder.Append(JsonConvert.SerializeObject(users));
            stringBuilder.Append("}");
            return stringBuilder.ToString();
        }
    }
}
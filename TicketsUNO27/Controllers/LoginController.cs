using AppRecordatorio.Models;
using Core.Models.User;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TicketsUNO27.Models.Security;

namespace TicketsUNO27.Controllers
{
    public class LoginController : Controller
    {
        private Context db = new Context();
    
        public ActionResult Login()
        {

            return View();
        }


        public async Task<ActionResult> Log(Users login)
        {
            //Traer Usuario e incluirle el Rol
            // Validamos que la contraseña no sea igual a la del password.
            Users us = new Users();
            us = await db.Users.Where(x => x.CC == login.CC && x.Password == login.Password).Include(x => x.Rol).FirstOrDefaultAsync();

            if (us is null)
            {
                ViewBag.Error = "Usuario o contraseña incorrectos";
                return View("Login");
            }
            Session["Photo"] = us.ProfilePhoto;
            Session["Rol"] = us.Rol.RolName;
            SessionPersister.Photo = us.ProfilePhoto;
            SessionPersister.UserName = us.UserName;
            FormsAuthentication.SetAuthCookie(SessionPersister.UserName, true);
            SessionPersister.Id = us.Id;
            //Session["Photo"] = SessionPersister.Photo;
            //return RedirectToAction("index");

            //string rol = (from d in db.Rols where (d.Id == us.RolId) select d.RolName).FirstOrDefault();
            if (Session["Rol"] !=null)
            {
                Session["Error"]= "User Developer";
                return RedirectToAction("Home", "Home", us.Id);
            }
            //else if (us.Rol.RolName.Equals("Admin"))
            //{
            //    Session["Error"] = "User Admin";
            //    var users = db.Users.Include(u => u.Rol);
            //    return RedirectToAction("HistoryProyectUser", "Home", us.Id);
            //}
            //else if (us.Rol.RolName.Equals("Desing"))
            //{
            //    Session["Error"] = "User Desing";
            //    return RedirectToAction("HistoryProyectUser", "Home", us.Id);
            //}
            //else if (us.Rol.RolName.Equals("Databases"))
            //{

            //    Session["Error"] = "USER Databases";
            //    return RedirectToAction("HistoryProyectUser", "Home", us.Id);
            //}
            return RedirectToAction("Home", "Home");
        }
    }
}
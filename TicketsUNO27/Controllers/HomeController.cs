using AppRecordatorio.Models;
using Core.Models.User;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using TicketsUNO27.Models.Security;

namespace TicketsUNO27.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private Context db = new Context();
        public async Task<ActionResult> Home()
        {
            ViewBag.UserName = SessionPersister.UserName;
            List<Projects> projects = new List<Projects>();
            List<Users_Groups> users_Groups= new List<Users_Groups>();
            users_Groups = await db.Users_Groups.Where(x => x.UserId == (Guid)SessionPersister.Id).Include(x => x.Groups).Include(x => x.Groups.Projects).ToListAsync();

            foreach (var item in users_Groups)
            {
                projects.Add(new Projects 
                {
                    Id = item.Groups.Projects.Id,
                    ProjectName = item.Groups.Projects.ProjectName,
                    ProjectDescription = item.Groups.Projects.ProjectDescription,
                    DisplayName= item.Groups.Projects.DisplayName
                });
            }
            
            return View (projects);
        }


        public async Task<ActionResult> HistoryProyectUser(Guid? id)
        {
            ViewBag.UserName = SessionPersister.UserName;
            var project = db.Projects.Where(x => x.Id == id).FirstOrDefault();
            Session["NameProject"] = project.DisplayName;
            Session["Idproject"] = project.Id;
            List<Histories> histories = new List<Histories>();
            histories = await db.Histories.Where(x => x.ProjectsId == id).ToListAsync();
            return View(histories);
        }

        public ActionResult Logout()
        {
            Session["NameProject"] = null;
            Session["Rol"] = null;
            Session["Photo"] = null;
            SessionPersister.UserName = string.Empty;
            SessionPersister.Id = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Login");
        
        }
    }
}
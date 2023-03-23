using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AppRecordatorio.Models;
using Core.Models.User;
using TicketsUNO27.Models.Security;


namespace TicketsUNO27.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private Context db = new Context();

        // GET: Users
        public ActionResult Index()
        {
            var users = db.Users.Include(u => u.Rol);
            return View(users.ToList());
        }
        // GET: Users_Filter
        public ActionResult IndexList()
        {
            //Se envia variable por TemData porque llega Nulo y se asigna a un var para no generar conflicto con Linq
            var id = (Guid)Session["IdDetails"];
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var groups_users = db.Users_Groups.Where(x => x.GroupId == id).ToList();
            List<Users> users = new List<Users>();
            foreach (var item in groups_users)
            {
                var users_list_temp = db.Users.Where(x => x.Id == item.UserId).Include(u => u.Rol);
                if (users_list_temp != null)
                {
                    users.AddRange(users_list_temp);
                }
            }
            if (users == null)
            {
                return HttpNotFound();
            }
            return View("Index",users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.UserName = SessionPersister.UserName;
            ViewBag.RolId = new SelectList(db.Rols, "Id", "RolName");
            return View();
        }

        // POST: Users/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUser([Bind(Include = "Id,UserName,LastName,NumberPhone,CC,Password,RolId,CreatedAt,UpdatedAt,DeletedAt,UpdatedBy,CreatedByName,UpdatedByName")] Users users, HttpPostedFile file)
        {
            if (ModelState.IsValid)
            {
                users.Id = Guid.NewGuid();
                users.CreatedByName = SessionPersister.UserName;
                users.CreatedBy = SessionPersister.Id;
                db.Users.Add(users);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RolId = new SelectList(db.Rols, "Id", "RolName", users.RolId);
            return View(users);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserName,LastName,NumberPhone,CC,Password,RolId,CreatedAt,UpdatedAt,DeletedAt,UpdatedBy,CreatedByName,UpdatedByName")] Users users , HttpPostedFileBase file)
        {
            if (users.UserName!=null && users.LastName != null && users.CC != null && users.Password != null && file!=null)
            {
                //Subir Foto y creacion de Url para el campo en el Usuario
                string path = Server.MapPath("~/ArchivosApp/");
                string name = Path.GetFileName(file.FileName);
                string extension = Path.GetExtension(file.FileName);
                var guidGen = Guid.NewGuid().ToString();
                string nameFile = guidGen + extension;
                var ruteToSavePhotho = Path.Combine(path, nameFile);
                file.SaveAs(ruteToSavePhotho);

                users.State = true;
                users.ProfilePhoto = ruteToSavePhotho;
                users.Id = Guid.NewGuid();
                users.CreatedAt = DateTime.Now;
                users.CreatedByName = SessionPersister.UserName;
                users.CreatedBy = SessionPersister.Id;
                db.Users.Add(users);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RolId = new SelectList(db.Rols, "Id", "RolName", users.RolId);
            return View(users);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            Session["Id"]= id;
            ViewBag.RolId = new SelectList(db.Rols, "Id", "RolName", users.RolId);
            return View(users);
        }

        // POST: Users/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserName,LastName,NumberPhone,CC,Password,RolId,CreatedAt,UpdatedAt,DeletedAt,CreatedBy,UpdatedBy,CreatedByName,UpdatedByName,State")] Users users)
        {
            Users users2 = db.Users.Find(Session["Id"]);
            users2.State = users.State;
            users2.UserName = users.UserName;
            users2.LastName = users.LastName;
            if (ModelState.IsValid)
            {
                users2.UpdatedByName = SessionPersister.UserName;
                users2.UpdatedBy= SessionPersister.Id;
                users.UpdatedAt = DateTime.Now;
                db.Entry(users2).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RolId = new SelectList(db.Rols, "Id", "RolName", users.RolId);
            return View(users);
        }
        // GET: Files/Photo
        public FileResult Photo()
        {
            if ((string)Session["Photo"]!=null)
            {
                return File((string)Session["Photo"], "application/pdf", "Documento" + ".jpg");
            }
            return File("~/Content/images/Logo-Circulo.png", "application/pdf", "Documento" + ".jpg");
        }
        
        //// GET: Files/Doc
        //public FileResult getPhoto()
        //{
        //    return File((string)Session["Photo"], "application/pdf", "Documento" + ".jpg");
        //}

        // GET: Users/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Users users = db.Users.Find(id);
            db.Users.Remove(users);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

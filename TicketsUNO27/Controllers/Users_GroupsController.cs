using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AppRecordatorio.Models;
using Core.Models.User;

namespace TicketsUNO27.Controllers
{
    [Authorize]
    public class Users_GroupsController : Controller
    {
        private Context db = new Context();

        // GET: Users_Groups
        public ActionResult Index()
        {
            var users_Groups = db.Users_Groups.Include(u => u.Groups).Include(u => u.Rol).Include(u => u.User);
            return View(users_Groups.ToList());
        }

        // GET: Users_Groups/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users_Groups users_Groups = db.Users_Groups.Find(id);
            if (users_Groups == null)
            {
                return HttpNotFound();
            }
            return View(users_Groups);
        }

        // GET: Users_Groups/Create
        public ActionResult Create()
        {
            ViewBag.GroupId = new SelectList(db.Groups, "Id", "GroupName");
            ViewBag.RolId = new SelectList(db.Rols, "Id", "RolName");
            ViewBag.UserId = new SelectList(db.Users, "Id", "UserName");
            return View();
        }

        // POST: Users_Groups/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,GroupId,RolId,CreatedAt,UpdatedAt,DeletedAt,CreatedBy,UpdatedBy,CreatedByName,UpdatedByName")] Users_Groups users_Groups)
        {
            if (ModelState.IsValid)
            {
                users_Groups.Id = Guid.NewGuid();
                db.Users_Groups.Add(users_Groups);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GroupId = new SelectList(db.Groups, "Id", "GroupName", users_Groups.GroupId);
            ViewBag.RolId = new SelectList(db.Rols, "Id", "RolName", users_Groups.RolId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "UserName", users_Groups.UserId);
            return View(users_Groups);
        }

        // GET: Users_Groups/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users_Groups users_Groups = db.Users_Groups.Find(id);
            if (users_Groups == null)
            {
                return HttpNotFound();
            }
            ViewBag.GroupId = new SelectList(db.Groups, "Id", "GroupName", users_Groups.GroupId);
            ViewBag.RolId = new SelectList(db.Rols, "Id", "RolName", users_Groups.RolId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "UserName", users_Groups.UserId);
            return View(users_Groups);
        }

        // POST: Users_Groups/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,GroupId,RolId,CreatedAt,UpdatedAt,DeletedAt,CreatedBy,UpdatedBy,CreatedByName,UpdatedByName")] Users_Groups users_Groups)
        {
            if (ModelState.IsValid)
            {
                db.Entry(users_Groups).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GroupId = new SelectList(db.Groups, "Id", "GroupName", users_Groups.GroupId);
            ViewBag.RolId = new SelectList(db.Rols, "Id", "RolName", users_Groups.RolId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "UserName", users_Groups.UserId);
            return View(users_Groups);
        }

        // GET: Users_Groups/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users_Groups users_Groups = db.Users_Groups.Find(id);
            if (users_Groups == null)
            {
                return HttpNotFound();
            }
            return View(users_Groups);
        }

        // POST: Users_Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Users_Groups users_Groups = db.Users_Groups.Find(id);
            db.Users_Groups.Remove(users_Groups);
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

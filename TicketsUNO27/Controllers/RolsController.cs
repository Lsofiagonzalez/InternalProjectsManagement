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
    public class RolsController : Controller
    {
        private Context db = new Context();

        // GET: Rols
        public ActionResult Index()
        {
            return View(db.Rols.ToList());
        }

        // GET: Rols/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rols rols = db.Rols.Find(id);
            if (rols == null)
            {
                return HttpNotFound();
            }
            return View(rols);
        }

        // GET: Rols/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Rols/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RolName,Description,DisplayName,ParentId,CreatedAt,UpdatedAt,DeletedAt,CreatedBy,UpdatedBy,CreatedByName,UpdatedByName")] Rols rols)
        {
            if (ModelState.IsValid)
            {
                rols.Id = Guid.NewGuid();
                db.Rols.Add(rols);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rols);
        }

        // GET: Rols/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rols rols = db.Rols.Find(id);
            if (rols == null)
            {
                return HttpNotFound();
            }
            return View(rols);
        }

        // POST: Rols/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RolName,Description,DisplayName,ParentId,CreatedAt,UpdatedAt,DeletedAt,CreatedBy,UpdatedBy,CreatedByName,UpdatedByName")] Rols rols)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rols).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rols);
        }

        // GET: Rols/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rols rols = db.Rols.Find(id);
            if (rols == null)
            {
                return HttpNotFound();
            }
            return View(rols);
        }

        // POST: Rols/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Rols rols = db.Rols.Find(id);
            db.Rols.Remove(rols);
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

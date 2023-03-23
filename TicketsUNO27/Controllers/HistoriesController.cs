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
using TicketsUNO27.Models.Security;

namespace TicketsUNO27.Controllers
{
    [Authorize]
    public class HistoriesController : Controller
    {
        private Context db = new Context();

        // GET: Histories
        public ActionResult Index()
        {
            var histories = db.Histories.Include(h => h.Projects);
            return View(histories.ToList());
        }

        // GET: Histories/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Histories histories = db.Histories.Find(id);
            if (histories == null)
            {
                return HttpNotFound();
            }
            return View(histories);
        }

        // GET: Histories/Create
        public ActionResult Create()
        {
            ViewBag.ProjectsId = new SelectList(db.Projects, "Id", "ProjectName");
            return View();
        }

        // POST: Histories/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,HistoryName,HistoryDescription,ProjectsId,CreatedAt,UpdatedAt,DeletedAt,CreatedBy,UpdatedBy,CreatedByName,UpdatedByName")] Histories histories)
        {
            histories.ProjectsId = (Guid)Session["Idproject"];
            if (ModelState.IsValid)
            {
                histories.CreatedBy = SessionPersister.Id;
                histories.UpdatedAt = DateTime.Now;
                histories.Id = Guid.NewGuid();
                db.Histories.Add(histories);
                db.SaveChanges();
                var list_histories = db.Histories.Where(x => x.CreatedBy == SessionPersister.Id).Include(h => h.Projects);
                return View("Index", list_histories.ToList());
            }
            ViewBag.ProjectsId = new SelectList(db.Projects.Where(x => x.CreatedBy== SessionPersister.Id), "Id", "ProjectName", histories.ProjectsId);
            return View(histories);
        }

        // GET: Histories/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Histories histories = db.Histories.Find(id);
            if (histories == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectsId = new SelectList(db.Projects, "Id", "ProjectName", histories.ProjectsId);
            return View(histories);
        }

        // POST: Histories/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,HistoryName,HistoryDescription,ProjectsId,CreatedAt,UpdatedAt,DeletedAt,CreatedBy,UpdatedBy,CreatedByName,UpdatedByName")] Histories histories)
        {
            if (ModelState.IsValid)
            {
                db.Entry(histories).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProjectsId = new SelectList(db.Projects, "Id", "ProjectName", histories.ProjectsId);
            return View(histories);
        }

        // GET: Histories/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Histories histories = db.Histories.Find(id);
            if (histories == null)
            {
                return HttpNotFound();
            }
            return View(histories);
        }

        // POST: Histories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Histories histories = db.Histories.Find(id);
            db.Histories.Remove(histories);
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

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
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
    public class FilesController : Controller
    {
        private Context db = new Context();

        // GET: Files
        public ActionResult Index()
        {
            var files = db.Files.Include(f => f.Histories);
            return View(files.ToList());
        }

        // GET: FilesHistory
        public ActionResult FilesHistory(Guid id)
        {
            var files = db.Files.Where(x => x.HistoriesId==id ).Include(f => f.Histories);
            return RedirectToAction("Index", files.ToList());
        }

        // GET: Files/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Files files = db.Files.Find(id);
            if (files == null)
            {
                return HttpNotFound();
            }
            return View(files);
        }

        // GET: Files/Create
        public ActionResult Create()
        {
            ViewBag.HistoriesId = new SelectList(db.Histories, "Id", "HistoryName");
            return View();
        }

        // POST: Files/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FileName,DisplayName,FileDescription,Url,HistoriesId,CreatedAt,UpdatedAt,DeletedAt,CreatedBy,UpdatedBy,CreatedByName,UpdatedByName")] Files files)
        {
            
            if (ModelState.IsValid)
            {
                files.Id = Guid.NewGuid();
                db.Files.Add(files);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.HistoriesId = new SelectList(db.Histories, "Id", "HistoryName", files.HistoriesId);
            return View(files);
        }

        // GET: Files/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Files files = db.Files.Find(id);
            if (files == null)
            {
                return HttpNotFound();
            }
            Session["EditId"] = id;
            ViewBag.HistoriesId = new SelectList(db.Histories, "Id", "HistoryName", files.HistoriesId);
            return View(files);
        }
        // GET: Files/Doc
        public FileResult Document(Guid id)
        {
            Files files = db.Files.Find(id);
            return File(files.Url, "application/pdf", "Documento"+files.Extension);
        }
        public ActionResult EditFile()
        {
            if (Session["EditId"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Files files = db.Files.Find(Session["EditId"]);
            if (files == null)
            {
                return HttpNotFound();
            } 
            Session["DeleteId"] = Session["EditId"];
            ViewBag.HistoriesId = new SelectList(db.Histories, "Id", "HistoryName", files.HistoriesId);
            return View(files);
        }
       
        // POST: Files/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FileName,DisplayName,FileDescription,Url,HistoriesId,CreatedAt,UpdatedAt,DeletedAt,CreatedBy,UpdatedBy,CreatedByName,UpdatedByName")] Files files)
        {
            //Datos que se deben poner por defecto se usa objeto auxiliar para evitar nulos
            Files fileComplete = db.Files.Find(Session["EditId"]);
            fileComplete.DisplayName = files.DisplayName;
            fileComplete.FileDescription = files.FileDescription;
            fileComplete.UpdatedAt = files.UpdatedAt;
            fileComplete.UpdatedBy = SessionPersister.Id;
            fileComplete.UpdatedByName = SessionPersister.UserName;
            if (ModelState.IsValid)
            {
                db.Entry(fileComplete).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.HistoriesId = new SelectList(db.Histories, "Id", "HistoryName", files.HistoriesId);
            return View(files);

        }

        // GET: Files/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Files files = db.Files.Find(id);
            if (files == null)
            {
                return HttpNotFound();
            }
            return View(files);
        }

        // POST: Files/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Files files = db.Files.Find(id);
            FileInfo file = new FileInfo(files.Url);
            try
            {
                file.Delete();
            }
            catch (Exception ex)
            {

            }
            
            db.Files.Remove(files);
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

        public void LoadFile([Bind(Include = "Id,FileName,DisplayName,FileDescription,Url,HistoriesId,CreatedAt,UpdatedAt,DeletedAt,CreatedBy,UpdatedBy,CreatedByName,UpdatedByName")] Files files)
        {

            if (Request.Files.Count > 0)
            {
                if (Session["DeleteId"]!=null)
                {
                    Files filesDelete = db.Files.Find(Session["DeleteId"]);
                    files.CreatedByName = filesDelete.CreatedByName;
                    files.CreatedBy = filesDelete.CreatedBy;
                    files.CreatedAt = filesDelete.CreatedAt;
                    files.UpdatedByName= SessionPersister.UserName;
                    files.UpdatedBy = SessionPersister.Id;
                    FileInfo file = new FileInfo(filesDelete.Url);
                    try
                    {
                        file.Delete();
                    }
                    catch (Exception ex)
                    {

                    }
                    db.Files.Remove(filesDelete);
                    Session["DeleteId"] = null;

                }
                HttpFileCollectionBase fileUpload = Request.Files;
                if (files.CreatedByName==null)
                {
                    files.CreatedByName= SessionPersister.UserName;
                    files.CreatedBy = SessionPersister.Id;
                    files.CreatedAt = DateTime.Now;
                }
                for (int i = 0; i < fileUpload.Count; i++)
                {

                    Files archivos = new Files();
                    string path = Server.MapPath("~/ArchivosApp/");
                    string name = Path.GetFileName(fileUpload[i].FileName);
                    string extension = Path.GetExtension(fileUpload[i].FileName);
                    var guidGen = Guid.NewGuid().ToString();

                    string nameFile = guidGen + extension;
                    var ruteToSavePDF = Path.Combine(path, nameFile);
                    fileUpload[i].SaveAs(ruteToSavePDF);


                    archivos.CreatedByName = files.CreatedByName;
                    archivos.CreatedBy = files.CreatedBy;
                    archivos.CreatedAt = files.CreatedAt;
                    archivos.DisplayName = files.DisplayName;
                    archivos.HistoriesId = new Guid("A92343F0-20BA-479B-A777-06721034862E");
                    archivos.FileName = name;
                    archivos.Url = ruteToSavePDF;
                    archivos.Extension = extension;
                    archivos.FileDescription = files.FileDescription;

                    db.Files.Add(archivos);
                    db.SaveChanges();
                }
                
            }

        }

    }
    
}

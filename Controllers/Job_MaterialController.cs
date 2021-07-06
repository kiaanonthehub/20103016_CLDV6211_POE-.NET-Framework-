using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CLDV_Framework.Models;

namespace CLDV_Framework.Controllers
{
    public class Job_MaterialController : Controller
    {
        private Domingo_Roof_WorksEntities db = new Domingo_Roof_WorksEntities();

        // GET: Job_Material
        public ActionResult Index()
        {
            var job_Material = db.Job_Material.Include(j => j.Job).Include(j => j.Material);
            return View(job_Material.ToList());
        }

        // GET: Job_Material/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job_Material job_Material = db.Job_Material.Find(id);
            if (job_Material == null)
            {
                return HttpNotFound();
            }
            return View(job_Material);
        }

        // GET: Job_Material/Create
        public ActionResult Create()
        {
            ViewBag.JobCardID = new SelectList(db.Jobs, "JobCardID", "JobCardID");
            ViewBag.MaterialID = new SelectList(db.Materials, "MaterialID", "Description");
            return View();
        }

        // POST: Job_Material/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JobMaterialID,JobCardID,MaterialID,Quantity")] Job_Material job_Material)
        {
            if (ModelState.IsValid)
            {
                db.Job_Material.Add(job_Material);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.JobCardID = new SelectList(db.Jobs, "JobCardID", "JobCardID", job_Material.JobCardID);
            ViewBag.MaterialID = new SelectList(db.Materials, "MaterialID", "Description", job_Material.MaterialID);
            return View(job_Material);
        }

        // GET: Job_Material/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job_Material job_Material = db.Job_Material.Find(id);
            if (job_Material == null)
            {
                return HttpNotFound();
            }
            ViewBag.JobCardID = new SelectList(db.Jobs, "JobCardID", "JobCardID", job_Material.JobCardID);
            ViewBag.MaterialID = new SelectList(db.Materials, "MaterialID", "Description", job_Material.MaterialID);
            return View(job_Material);
        }

        // POST: Job_Material/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JobMaterialID,JobCardID,MaterialID,Quantity")] Job_Material job_Material)
        {
            if (ModelState.IsValid)
            {
                db.Entry(job_Material).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.JobCardID = new SelectList(db.Jobs, "JobCardID", "JobCardID", job_Material.JobCardID);
            ViewBag.MaterialID = new SelectList(db.Materials, "MaterialID", "Description", job_Material.MaterialID);
            return View(job_Material);
        }

        // GET: Job_Material/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job_Material job_Material = db.Job_Material.Find(id);
            if (job_Material == null)
            {
                return HttpNotFound();
            }
            return View(job_Material);
        }

        // POST: Job_Material/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Job_Material job_Material = db.Job_Material.Find(id);
            db.Job_Material.Remove(job_Material);
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

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
    public class Job_EmployeeController : Controller
    {
        private Domingo_Roof_WorksEntities db = new Domingo_Roof_WorksEntities();

        // GET: Job_Employee
        public ActionResult Index()
        {
            var job_Employee = db.Job_Employee.Include(j => j.Employee).Include(j => j.Job);
            return View(job_Employee.ToList());
        }

        // GET: Job_Employee/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job_Employee job_Employee = db.Job_Employee.Find(id);
            if (job_Employee == null)
            {
                return HttpNotFound();
            }
            return View(job_Employee);
        }

        // GET: Job_Employee/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "Name");
            ViewBag.JobCardID = new SelectList(db.Jobs, "JobCardID", "JobCardID");
            return View();
        }

        // POST: Job_Employee/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JobEmployeeID,JobCardID,EmployeeID")] Job_Employee job_Employee)
        {
            if (ModelState.IsValid)
            {
                db.Job_Employee.Add(job_Employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "Name", job_Employee.EmployeeID);
            ViewBag.JobCardID = new SelectList(db.Jobs, "JobCardID", "JobCardID", job_Employee.JobCardID);
            return View(job_Employee);
        }

        // GET: Job_Employee/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job_Employee job_Employee = db.Job_Employee.Find(id);
            if (job_Employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "Name", job_Employee.EmployeeID);
            ViewBag.JobCardID = new SelectList(db.Jobs, "JobCardID", "JobCardID", job_Employee.JobCardID);
            return View(job_Employee);
        }

        // POST: Job_Employee/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JobEmployeeID,JobCardID,EmployeeID")] Job_Employee job_Employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(job_Employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "Name", job_Employee.EmployeeID);
            ViewBag.JobCardID = new SelectList(db.Jobs, "JobCardID", "JobCardID", job_Employee.JobCardID);
            return View(job_Employee);
        }

        // GET: Job_Employee/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job_Employee job_Employee = db.Job_Employee.Find(id);
            if (job_Employee == null)
            {
                return HttpNotFound();
            }
            return View(job_Employee);
        }

        // POST: Job_Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Job_Employee job_Employee = db.Job_Employee.Find(id);
            db.Job_Employee.Remove(job_Employee);
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

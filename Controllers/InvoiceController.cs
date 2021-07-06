using CLDV_Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CLDV_Framework.Controllers
{
    public class InvoiceController : Controller
    {

        Domingo_Roof_WorksEntities db = new Domingo_Roof_WorksEntities();

        public ActionResult Index()
        {
            List<Employee> employees = db.Employees.ToList();
            List<Customer> customers = db.Customers.ToList();
            List<Job> jobs = db.Jobs.ToList();
            List<Material> materials = db.Materials.ToList();
            List<Job_Material> job_Materials = db.Job_Material.ToList();
            List<Job_Employee> job_Employees = db.Job_Employee.ToList();
            List<JobType> jobTypes = db.JobTypes.ToList();

            IEnumerable<Invoice> JobCard =
                (
                from j in jobs
                join c in customers on j.CustomerID equals c.CustomerID into T1
                from c in T1.ToList()
                join jt in jobTypes on j.JobTypeID equals jt.JobTypeID into T4
                from jt in T4.ToList()
                select new Invoice
                {
                    Jobs = j,
                    Customer = c,
                    JobType = jt,
                });

            return View(JobCard);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            using (Domingo_Roof_WorksEntities db = new Domingo_Roof_WorksEntities())
            {
                List<Employee> employees = db.Employees.ToList();
                List<Customer> customers = db.Customers.ToList();
                List<Job> jobs = db.Jobs.ToList();
                List<Material> materials = db.Materials.ToList();
                List<Job_Material> job_Materials = db.Job_Material.ToList();
                List<Job_Employee> job_Employees = db.Job_Employee.ToList();
                List<JobType> jobTypes = db.JobTypes.ToList();

                List<Invoice> JobCard =
                    (
                    from j in jobs
                    where j.JobCardID.Equals(id)
                    join c in customers on j.CustomerID equals c.CustomerID into T1
                    from c in T1.ToList()
                    join je in job_Employees on j.JobCardID equals je.JobCardID into T2
                    from je in T2.ToList()
                    join e in employees on je.EmployeeID equals e.EmployeeID into T3
                    from e in T3.ToList()
                    join jt in jobTypes on j.JobTypeID equals jt.JobTypeID into T4
                    from jt in T4.ToList()
                    join jm in job_Materials on j.JobCardID equals jm.JobCardID into T5
                    from jm in T5.ToList()
                    join m in materials on jm.MaterialID equals m.MaterialID into T6
                    from m in T6.ToList()

                    select new Invoice
                    {
                        Jobs = j,
                        Customer = c,
                        Material = m,
                        Job_Material = jm,
                        JobType = jt,
                        Job_Employee = je,
                        Employee = e

                    }).ToList();

                List<string> Employees = new List<string>();
                List<string> Materials = new List<string>();

                foreach (Invoice item in JobCard)
                {
                    if (item.Jobs.JobCardID == id)
                    {
                        Materials.Add(" "+item.Job_Material.Quantity + " x " + item.Material.Description);

                        Employees.Add(" "+item.Employee.EmployeeID + " " + item.Employee.Name + " " + item.Employee.Surname);
                    }
                }

                foreach (Invoice item in JobCard)
                {
                    if (item.Jobs.JobCardID == id)
                    {
                        ViewBag.JobCardID = item.Jobs.JobCardID;
                        ViewBag.CustomerName = item.Customer.Name + " " + item.Customer.Surname;
                        ViewBag.Address = item.Customer.Address + ", " + item.Customer.City + "," + item.Customer.PostalCode;
                        ViewBag.JobType = item.JobType.JobType1;
                        ViewBag.Days = item.Jobs.Days;
                        ViewBag.Rate = item.JobType.Rate;
                        ViewBag.TotalExclVat = (item.JobType.Rate * item.Jobs.Days);
                        ViewBag.VAT = Convert.ToDouble(item.JobType.Rate * item.Jobs.Days) * 0.14;
                        ViewBag.TotalInclVat = Convert.ToDouble(item.JobType.Rate * item.Jobs.Days) * 1.14;

                    }
                }

                List<string> dplEmployees = new List<string>();
                List<string> dplMaterials = new List<string>();

                for (int i = 0; i < Employees.Count; i++)
                {
                    if (!dplEmployees.Contains(Employees[i]))
                    {
                        dplEmployees.Add(Employees[i]);
                    }
                }

                for (int i = 0; i < Materials.Count; i++)
                {
                    if (!dplMaterials.Contains(Materials[i]))
                    {
                        dplMaterials.Add(Materials[i]);
                    }
                }

                ViewBag.Employees = dplEmployees;
                ViewBag.Materials = dplMaterials;
                if (JobCard == null)
                {
                    return HttpNotFound();
                }

                return View(JobCard);

            }
        }
    }
}
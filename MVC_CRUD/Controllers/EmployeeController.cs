using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_CRUD.Models;

namespace MVC_CRUD.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetData()
        {
            using (DBModel db = new DBModel())
            {
                List<Employee_tbl> empList = db.Employee_tbl.ToList<Employee_tbl>();
                return Json(new { data = empList }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0) { 
            return View(new Employee_tbl());
            }
            else
            {
                using (DBModel db = new DBModel())
                {
                    return View(db.Employee_tbl.Where(x => x.EmployeeID == id).FirstOrDefault<Employee_tbl>());
                }
            }
        }
        [HttpPost]
        public ActionResult AddOrEdit(Employee_tbl emp)
        {
            using (DBModel db = new DBModel())
            {
                if(emp.EmployeeID == 0)
                { 
                db.Employee_tbl.Add(emp);
                db.SaveChanges();
                return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    db.Entry(emp).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
                }
            }
            
        }
        public ActionResult Delete(int id)
        {
            using (DBModel db = new DBModel())
            {
                Employee_tbl emp = db.Employee_tbl.Where(x => x.EmployeeID == id).FirstOrDefault<Employee_tbl>();
                db.Employee_tbl.Remove(emp);
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted Succesfully" }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}
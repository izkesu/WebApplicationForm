using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication4.Controllers
{
    public class HomeController : Controller
    {
        demoEntities _contxt = new demoEntities();
        demoEntities c = new demoEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult applicationform()
        {
            var data = _contxt.Table_1.ToList();

            ViewBag.emp = data;
            return View();
        }

        [HttpPost]
        public ActionResult Applicationform(String name, String email, String address, String state, String city)
        {

            var Yn = _contxt.Table_1.Any(x => x.email == email);
            if (Yn == false)
            {
                Table_1 obj = new Table_1();
                obj.id = Guid.NewGuid();
                obj.name = name;
                obj.email = email;
                obj.address = address;
                obj.state = state;
                obj.city = city;

                _contxt.Table_1.Add(obj);
                _contxt.SaveChanges();
            }
            else
            {
                var existEmp = _contxt.Table_1.Where(x => x.email == email).FirstOrDefault();
                existEmp.name = name;
                existEmp.email = email;
                existEmp.address = address;
                existEmp.state = state;
                existEmp.city = city;
                _contxt.SaveChanges();

            }

//commit
            return Json("");
        }

        public ActionResult fetch(Guid? id)
        {
            var employeeData = c.Table_1.FirstOrDefault(x => x.id == id);

            return Json(employeeData);
        }





        [HttpPost]

        public ActionResult DeleteEmployee(Guid id)
        {

            var employee = _contxt.Table_1.FirstOrDefault(x => x.id == id);
            if (employee != null)
            {
                _contxt.Table_1.Remove(employee);
                _contxt.SaveChanges();
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, message = "Employees details not found." });
            }
        }

    }

}


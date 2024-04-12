using System;
using System.Linq;
using System.Web.Mvc;

using System.Data.Entity;
using WebApplication4.Models;
using System.Net;
using System.IO;
using System.Web;

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

        //========================================================================================================
        public ActionResult applicationform()
        {
            var data = _contxt.Table_1.ToList();

            ViewBag.emp = data;
            return View();
        }

        //========================================================================================================

        [HttpPost]

        public ActionResult ApplicationForm(String name, String email, String address, String state, String city)
        {
            string fileName = "";
            if (Request.Files.Count > 0)
            {
                HttpFileCollectionBase files = Request.Files;
                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFileBase file = files[i];
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testFiles = file.FileName.Split(new char[] { '\\' });
                        fileName = testFiles[testFiles.Length - 1];
                    }
                    else
                    {
                        fileName = file.FileName;
                    }

                    // Save the file to a specific directory
                    string filePath = Path.Combine(Server.MapPath("~/Content/pictures"), fileName);
                    file.SaveAs(filePath);
                }
            }

           
            var exists = _contxt.Table_1.Any(x => x.email == email);
            if (!exists)
            {
                Table_1 obj = new Table_1
                {
                    id = Guid.NewGuid(),
                    name = name,
                    email = email,
                    address = address,
                    state = state,
                    city = city,
                    image = fileName // Save the file name, not the path
                };

                _contxt.Table_1.Add(obj);
            }
            else
            {
                var existingRecord = _contxt.Table_1.FirstOrDefault(x => x.email == email);
                if (existingRecord != null)
                {
                    existingRecord.name = name;
                    existingRecord.email = email;
                    existingRecord.address = address;
                    existingRecord.state = state;
                    existingRecord.city = city;
                    existingRecord.image = fileName; // Update the file name
                }
            }

            _contxt.SaveChanges();
            return Json("");
        }

        //hi

        //================================================================================================





        public ActionResult fetch(Guid? id)
        {
            var employeeData = c.Table_1.FirstOrDefault(x => x.id == id);

            return Json(employeeData);
        }









    

       //=========================================================================================================
        public ActionResult SavedDetails()
        {
          
           var data = _contxt.Table_1.ToList();


            ViewBag.emp = data;
            return View();
        }

        //============================================================================================================




     





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



        public ActionResult DisplayImage(Guid id)
        {
            var employee = _contxt.Table_1.FirstOrDefault(x => x.id == id);
            if (employee != null && !string.IsNullOrEmpty(employee.image))
            {
                var imagePath = Path.Combine(Server.MapPath("~/Content/pictures"), employee.image);
                var imageBytes = System.IO.File.ReadAllBytes(imagePath);
                return File(imageBytes, "image/jpeg"); 
            }
            else
            {
                return HttpNotFound();
            }
        }









        [HttpPost]
        public ActionResult UpdateEmployee(Employee employee)
        {
            if (ModelState.IsValid)
            {

                
                var existingEmployee = _contxt.Table_1.FirstOrDefault(x => x.id == employee.id);

                if (existingEmployee != null)
                {
                    existingEmployee.name = employee.Name;
                    existingEmployee.email = employee.Email;
                    existingEmployee.address = employee.Address;
                    existingEmployee.state = employee.State;
                    existingEmployee.city = employee.City;

                    _contxt.Entry(existingEmployee).State = EntityState.Modified;
                    _contxt.SaveChanges();

                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false, message = "Employee not found." });
                }
            }
            else
            {
                return Json(new { success = false, message = "Invalid data." });
            }
        }

       
    }

}




using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication1.Models;


namespace MvcApplication1.Controllers
{
    public class EmployeesController : Controller
    {
        private EmployeeDBContext db = new EmployeeDBContext();

        //
        // GET: /Employees/
        [HttpGet]
        public ActionResult Index()
        {
           
            var EmployeeList = new List<string>();                  //Create a list for drop down box (Location)
            var EmployeeList2 = new List<string>();                 //Create a list for drop down box (Department)

            var EmployeeQry = from d in db.Employees                //Obtains all data in the Location Column 
                              orderby d.Location                    //....Stores in list
                              select d.Location;
            EmployeeList.AddRange(EmployeeQry.Distinct());
            ViewBag.employeeLocation = new SelectList(EmployeeList);

            var EmployeeQry2 = from c in db.Employees               //Obtains all data in the Department column 
                               orderby c.Department                  //...stores in List
                               select c.Department;
            EmployeeList2.AddRange(EmployeeQry2.Distinct());
            ViewBag.employeeDepartment = new SelectList(EmployeeList2);

            var qry = from b in db.Employees
                      orderby b.LastName ascending
                      select b;

            return View(qry.ToList());                              //returns list to index view
        }


        //Searching index Overload
        public ActionResult Index(string searchDate, string employeeLocation, string searchStringFirst, string searchStringLast,
                                        string employeeDepartment, string datepick, string endDate)
        {
            var EmployeeList = new List<string>();                  //Create a list for drop down box (Location)
            var EmployeeList2 = new List<string>();                 //Create a list for drop down box (Department)


            var EmployeeQry = from d in db.Employees                //Obtains all data in the Location Column 
                              orderby d.Location                    //....Stores in list
                              select d.Location;
            EmployeeList.AddRange(EmployeeQry.Distinct());
            ViewBag.employeeLocation = new SelectList(EmployeeList);

            var EmployeeQry2 = from c in db.Employees               //Obtains all data in the Department column 
                               orderby c.Department                  //...stores in List
                               select c.Department;
            EmployeeList2.AddRange(EmployeeQry2.Distinct());
            ViewBag.employeeDepartment = new SelectList(EmployeeList2);

            var EmployeeQry3 = from b in db.Employees
                               where b.DateAdded <= DateTime.Now.Date
                               select b;



            var employees = from m in db.Employees                  //Get data from Employees Database
                            select m;

            if (!String.IsNullOrEmpty(searchStringFirst))           //If the searchString parameter is NOT empty:
            {                                                       //...sets current 'employees' to all records containing
                employees = employees.Where(s => s.FirstName.Contains(searchStringFirst)).OrderBy(r => r.LastName); //...searchString in Name Column
            }

            if (!String.IsNullOrEmpty(searchStringLast))            //If the searchString parameter is NOT empty:
            {                                                       //...sets current 'employees' to all records containing
                employees = employees.Where(s => s.LastName.Contains(searchStringLast)).OrderBy(r => r.LastName); //...searchString in Name Column
            }
            if (!String.IsNullOrEmpty(employeeLocation))            //If the employeeLocation parameter is NOT empty:
            {                                                       //....sets current 'employees' to all records containing
                employees = employees.Where(t => t.Location.Contains(employeeLocation)).OrderBy(s => s.LastName);//...employeeLocation in Location column
            }

            if (!String.IsNullOrEmpty(employeeDepartment))          //If the employeeDepartment parameter is NOT empty:
            {                                                       //...sets current 'employees' to all records containing
                employees = employees.Where(x => x.Department == (employeeDepartment)).OrderBy(r => r.LastName);//...employeDepartment in Department column
            }

            if (!String.IsNullOrEmpty(datepick))                  //If the datepicker parameter is NOT empty:
            {
                var radio = searchDate;
                string dateString = datepick;                     //Sets datepicker as the string dateString
                DateTime dateFromString = DateTime.Parse(dateString, System.Globalization.CultureInfo.InvariantCulture); //Parses dateString into format Datetime (Date only)

                if (searchDate == "onDate")                       //if onDate radiobutton is selected
                {                                                 //return employees that have a DateAdded that equals the date indicated
                    return View(employees.Where(x => x.DateAdded == (dateFromString)).OrderBy(r => r.LastName));
                }                                                
                if (searchDate == "beforeDate")                   //if beforeDate radiobutton is selected
                {                                                 //return employees that have a DateAdded before the date indicated
                    return View(employees.Where(x => x.DateAdded < (dateFromString)).OrderBy(r => r.LastName));
                }                                                
                if (searchDate == "afterDate")                    //if aferDate radiobutton is selected
                {                                                 //return employees that have a DateAdded after the date indicated
                    return View(employees.Where(x => x.DateAdded > (dateFromString)).OrderBy(r => r.LastName));
                }
                else                                              //else (betweenDate) [this might have to be tweaked-works for now]
                {
                    string dateString2 = endDate;                 //sets dateString2 to what is in the endDate textbox
                    DateTime dateFromString2 = DateTime.Parse(endDate, System.Globalization.CultureInfo.InvariantCulture); //Parses dateString into format Datetime (Date only)
                    var emp2 = employees.Where(x => x.DateAdded >= (dateFromString)); //sets emp2 to employees that have DateAdded on or after startDate (datefromstring)
                    return View(emp2.Where(x => x.DateAdded <= (dateFromString2)).OrderBy(r => r.LastName)); //returns emp2 (employees) where dateAdded is ALSO before or on endDate
                    
                }
            }

            else
            {
                return View(employees.OrderBy(r => r.LastName));         //Else just display employees
            }
        }


        //
        // GET: /Employees/Details/5

        public ActionResult Details(int id = 0)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        //
        // GET: /Employees/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Employees/Create

        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        //
        // GET: /Employees/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
          
            return View(employee);
        }

        //
        // POST: /Employees/Edit/5

        [HttpPost]
        public ActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        //
        // GET: /Employees/Delete/5

        public ActionResult DeleteRecord(int id)
        {
            Employee employee = db.Employees.Where(m => m.ID == id).FirstOrDefault();
            if (employee != null)
            {
                try
                {
                    db.Employees.Remove(employee);
                    db.SaveChanges();
                }
                catch { }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id = 0)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        //
        // POST: /Employees/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        
       
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult AwaitingApproval() 
        {
            return View();
        }

        //  ==============  Filter Database Results ============  Not used === keep for reference //
    /*    public ActionResult SearchIndex(string searchDate, string employeeLocation, string searchStringFirst, string searchStringLast,
                                        string employeeDepartment, string datepick, string endDate)
        {
            var EmployeeList = new List<string>();                  //Create a list for drop down box (Location)
            var EmployeeList2 = new List<string>();                 //Create a list for drop down box (Department)


            var EmployeeQry = from d in db.Employees                //Obtains all data in the Location Column 
                              orderby d.Location                    //....Stores in list
                              select d.Location;
            EmployeeList.AddRange(EmployeeQry.Distinct());
            ViewBag.employeeLocation = new SelectList(EmployeeList);

            var EmployeeQry2 = from c in db.Employees               //Obtains all data in the Department column 
                               orderby c.Department                  //...stores in List
                               select c.Department;
            EmployeeList2.AddRange(EmployeeQry2.Distinct());
            ViewBag.employeeDepartment = new SelectList(EmployeeList2);

            var EmployeeQry3 = from b in db.Employees
                               where b.DateAdded <= DateTime.Now.Date
                               select b;

            

            var employees = from m in db.Employees                  //Get data from Employees Database
                            select m;

            if (!String.IsNullOrEmpty(searchStringFirst))           //If the searchString parameter is NOT empty:
            {                                                       //...sets current 'employees' to all records containing
                employees = employees.Where(s => s.FirstName.Contains(searchStringFirst)).OrderBy(r => r.LastName); //...searchString in Name Column
            }

            if (!String.IsNullOrEmpty(searchStringLast))            //If the searchString parameter is NOT empty:
            {                                                       //...sets current 'employees' to all records containing
                employees = employees.Where(s => s.LastName.Contains(searchStringLast)).OrderBy(r => r.LastName); //...searchString in Name Column
            }
            if (!String.IsNullOrEmpty(employeeLocation))            //If the employeeLocation parameter is NOT empty:
            {                                                       //....sets current 'employees' to all records containing
                employees = employees.Where(t => t.Location.Contains(employeeLocation)).OrderBy(s => s.LastName);//...employeeLocation in Location column
            }

            if (!String.IsNullOrEmpty(employeeDepartment))          //If the employeeDepartment parameter is NOT empty:
            {                                                       //...sets current 'employees' to all records containing
                employees = employees.Where(x => x.Department == (employeeDepartment)).OrderBy(r => r.LastName);//...employeDepartment in Department column
            }

            if (!String.IsNullOrEmpty(datepick))                  //If the datepicker parameter is NOT empty:
            {
                var radio = searchDate;
                string dateString = datepick;                     //Sets datepicker as the string dateString
                DateTime dateFromString = DateTime.Parse(dateString, System.Globalization.CultureInfo.InvariantCulture); //Parses dateString into format Datetime (Date only)



                if (searchDate == "onDate") 
                {
                    return View(employees.Where(x => x.DateAdded == (dateFromString)).OrderBy(r => r.LastName));
                }
                if (searchDate == "beforeDate")
                {
                    return View(employees.Where(x => x.DateAdded < (dateFromString)).OrderBy(r => r.LastName));
                }
                if (searchDate == "afterDate")
                {
                    return View(employees.Where(x => x.DateAdded > (dateFromString)).OrderBy(r => r.LastName));
                }
                else
                 {

                     string dateString2 = endDate;
                     DateTime dateFromString2 = DateTime.Parse(endDate, System.Globalization.CultureInfo.InvariantCulture); //Parses dateString into format Datetime (Date only)
                    var emp2 = employees.Where(x => x.DateAdded >= (dateFromString));
                    emp2 = emp2.Where(x => x.DateAdded <= (dateFromString2)).OrderBy(r => r.LastName);
                     return View(emp2);
                      
                 }
                 
                
            }

            else
            {
                return View(employees.OrderBy(r => r.LastName));         //Else just display employees
            }
        } */
    }
}
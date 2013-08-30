using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication1.Models;

namespace MvcApplication1.Models
{
    public class Employee
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "First name is required")]                                     //Validation rules for FirstName
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Valid characters:<br/> A-Z, a-z")]    //(letters only) and REQUIRED
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required")]                                      //Validation rules for LastName
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Valid characters:<br/> A-Z, a-z")]    //(letters only) and REQUIRED
        public string LastName { get; set; }

        [Required(ErrorMessage = "A location is required")]                                     //Validation rules for Location
        [StringLength(3)]                                                                       //Three Chars only and REQUIRED
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Valid characters:<br/> A-Z, a-z")]    //(letters only)
        public string Location { get; set; }

        public string Department { get; set; }

        [RegularExpression(@"^\(?([0-9]{3})\)?[-]?([0-9]{3})[-]?([0-9]{4})$", ErrorMessage = "Format xxx-xxx-xxxx<br/>Numbers only.")]
        public string Phone { get; set; }                                                       //Valiation rules for Phone
                                                                                                //(numbers and dashes) 
        [RegularExpression(@"^\(?([0-9]{3})\)?[-]?([0-9]{3})[-]?([0-9]{4})$", ErrorMessage = "Format xxx-xxx-xxxx<br/>Numbers only.")]
        public string Cell { get; set; }                                                        //Valiation rules for Cell
                                                                                                //(numbers and dashes) 
        [RegularExpression(@"^\(?([0-9]{3})\)?[-]?([0-9]{3})[-]?([0-9]{4})$", ErrorMessage = "Format xxx-xxx-xxxx<br/>Numbers only.")]
        public string Fax { get; set; }                                                         //Valiation rules for Fax
                                                                                                //(numbers and dashes) 
        [RegularExpression("^[0-9]*$", ErrorMessage = "Numeric values only.")]                  //Validation rules for Room
        public string Room { get; set; }                                                        //Numbers only

        [RegularExpression("^[0-9]*$", ErrorMessage = "Numeric values only.")]                   //Validation rules for Extension
        public string Extension { get; set; }                                                    //Numbers only

        [DataType(DataType.Date)]                                                                //Validation rules for DateAdded
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]                                     //Sets date format
        public DateTime DateAdded { get; set; }


}

    public class EmployeeDBContext : DbContext                                                  
    {
        public DbSet<Employee> Employees { get; set; }
    }
}
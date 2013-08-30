using MvcApplication1.Models;

namespace MvcApplication1.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MvcApplication1.Models.EmployeeDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MvcApplication1.Models.EmployeeDBContext context)
        {
            context.Employees.AddOrUpdate(i => i.FirstName,
                new Employee
                {
                    FirstName = "John",
                    LastName = "Smith",
                    Location = "CO",
                    Department = "IT",
                    Phone = "306-555-5555",
                    Cell = "306-555-5847",
                    Fax = "306-555-5556",
                    Room = "203",
                    Extension = "4561",
                    DateAdded = DateTime.Parse("1989-1-11")
                }

           );

        }
    }
}

using System;
using InternetERP.Data;
using InternetERP.Models;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new InternetERPDbContext();
            //db.Database.EnsureDeleted();
            //db.Database.EnsureCreated();

            //var employee = new Employee
            //{
            //    FullName = "Методи Личков",
            //    HireDate = DateTime.UtcNow,
            //    Salary = 1500,
            //    JobbTitle = new JobTitle
            //    {
            //        Name = "Мениджър"0
            //    },
            //    Role =  new Role
            //    {
            //        Name = "Администратор"
            //    }
                
            //};
            //var town = new Town
            //{
            //    Name = "Пазарджик"
            //};
            //var address = new Address
            //{
            //    District = "Чиксалам",
            //    Street = "Георги Бенковски 60",
            //    Note = "Учи EF Core",
            //    Town = new Town
            //    {
            //        Name = "Пазарджик"
            //    },
                
            //};
            //employee.Address = address;
            //db.Employees.Add(employee);
            db.SaveChanges();

        }
    }
}

using InternetERP.Data;
using InternetERP.Initializer.Generators;
using InternetERP.Models;
using System;

namespace InternetERP.Initializer
{
    public class DbInitializer
    {
        public static void ResetDatabase(InternetERPDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            Console.WriteLine("BookShop database created successfully.");

            Seed(context);
        }

        public static void Seed(InternetERPDbContext context)
        {
            //Book[] books = BookGenerator.CreateBooks();

            //context.Books.AddRange(books);

            Employee[] employees = EmployeeGenerator.CreateBooks();

            context.Employees.AddRange(employees);


            context.SaveChanges();

            Console.WriteLine("Sample data inserted successfully.");
        }


    }
}

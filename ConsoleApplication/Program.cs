using System;
using InternetERP.Data;
using InternetERP.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new InternetERPDbContext();
            
            db.Database.Migrate();
            // test
            //db.SaveChanges();

        }
    }
}

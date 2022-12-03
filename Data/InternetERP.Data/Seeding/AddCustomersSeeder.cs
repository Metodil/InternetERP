namespace InternetERP.Data.Seeding
{
    using System;
    using System.Threading.Tasks;

    using InternetERP.Data.Models;

    public class AddCustomersSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            for (int i = 0; i < 12; i++)
            {
                await AddCustomer(dbContext, i);
            }
        }

        private static async Task AddCustomer(ApplicationDbContext dbContext, int i)
        {
            var id = i.ToString("D2");
            var newCustomer = new Customer
            {
                Name = "NameOfCustomer-" + id,
                TownId = 1, // Sofia
                Address = GetAddresSeeder(),
                VATNumber = "BG2010000" + id,
                BulstatNumber = "8220000" + id,
                MOL = "MOLName" + id,
                ReceivedFrom = "ReceivedName-" + id,
            };
            await dbContext.Customers.AddAsync(newCustomer);
        }

        private static string GetAddresSeeder()
        {
            var addresses = new string[]
            {
            "ul. YERUSALIM, 1 BL.24",
            "ul. YANKO SAKAZOV, 56",
            "ul. GRAF IGNATIEV, 7",
            "ul. TOTLEBEN, 8",
            "ul. G.M.DIMITROV 94",
            "ul. P. YAVOROV, 2",
            "ul. TSARIGRADSKO SHOSE 117",
            "ul. SERDIKA, 4",
            "ul. TSAR BORIS III, 1",
            "ul. HAYDUSHKA POLYANA, 8",
            };
            Random random = new Random();
            int index = random.Next(0, addresses.Length);
            return addresses[index];
        }
    }
}

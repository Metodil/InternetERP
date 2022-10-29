namespace InternetERP.Data.Seeding.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using InternetERP.Data.Models;

    internal class InternetAccountTypeSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.InternetAccountTypes.Any())
            {
                return;
            }

            await dbContext.InternetAccountTypes.AddAsync(new InternetAccountType { Name = "Dial-Up", Description = "Internet accounts are called \"dial-up\" accounts because you use them by \"dialing up\" the Internet provider through your modem and telephone line." });
            await dbContext.InternetAccountTypes.AddAsync(new InternetAccountType { Name = "Cable Internet", Description = "Supplied by your local cable TV company, cable Internet enters your house through the same cable that TV signals travel through." });
            await dbContext.InternetAccountTypes.AddAsync(new InternetAccountType { Name = "DSL", Description = "Used through your regular phone line, a Digital Subscriber Line (DSL; also known by various other abbreviations such as ADSL or xDSL) account is supplied by, or in cooperation with, your local telephone company." });
            await dbContext.InternetAccountTypes.AddAsync(new InternetAccountType { Name = "Email-Only", Description = "With an email-only account, you get full access to Internet email, and nothing else—no Web, no newsgroups, no chat, no shoes, no shirt, no service." });

            await dbContext.SaveChangesAsync();
        }
    }
}

namespace InternetERP.Services.Data.Tests
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

    using InternetERP.Data;
    using InternetERP.Services.Data.ContactUs;
    using InternetERP.Services.Data.Employee;
    using InternetERP.Services.Mapping;
    using InternetERP.Services.Messaging;
    using InternetERP.Web.ViewModels.Contact;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;
    using Xunit.Sdk;

    public class ContactUsServiceTests
    {
        [Fact]
        public async Task SendToUsAsyncRerurnTrueWhenMailIsSend()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var db = new ApplicationDbContext(options);
            var emailSender = new Mock<IEmailSender>();

            var service = new ContactUsService(emailSender.Object);

            var input = new ContactUsViewModel
            {
                Name = "Name",
                Email = "Email",
                Subject = "Subject",
                Content = "Content",
            };
            var result = await service.SendToUsAsync(input);

            Assert.True(result);
        }
     }
}

namespace InternetERP.Services.Data.ContactUs.Contracts
{
    using System.Threading.Tasks;

    using InternetERP.Web.ViewModels.Contact;

    public interface IContactUsService
    {
        Task<bool> SendToUsAsync(ContactUsViewModel input);

        // Task<bool> MarkAsAnsweredAsync(int emailId);

        // Task<NotAnsweredEmailsOutputViewModel> GetByIdAsync(int emailId);

        // IEnumerable<NotAnsweredEmailsOutputViewModel> GetAllNotAnswered();

        // Task<CountEmailsOutputViewModel> GetAllCountAsync();

        // IDictionary<DateTime, int> GetNotAnsweredLast10Days();
    }
}

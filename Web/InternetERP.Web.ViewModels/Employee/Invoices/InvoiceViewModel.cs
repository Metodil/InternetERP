namespace InternetERP.Web.ViewModels.Employee.Invoices
{
    using System.Collections.Generic;

    using InternetERP.Data.Models;

    public class InvoiceViewModel
    {
        public Invoice InvoiceInfo { get; set; }

        public Customer Customer { get; set; }
    }
}

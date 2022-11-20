namespace InternetERP.Web.ViewModels.Administration.Towns
{
    using System;

    using InternetERP.Data.Models;
    using InternetERP.Services.Mapping;

    public class TownListViewModel : IMapFrom<Town>
    {
        public string Name { get; set; }

        public string Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}

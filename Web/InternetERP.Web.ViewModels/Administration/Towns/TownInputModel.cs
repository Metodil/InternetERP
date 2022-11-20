namespace InternetERP.Web.ViewModels.Administration.Towns
{
    using System.ComponentModel.DataAnnotations;

    using InternetERP.Common;
    using InternetERP.Data.Models;
    using InternetERP.Services.Mapping;

    public class TownInputModel : IMapTo<Town>
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = GlobalConstants.TextError, MinimumLength = 2)]
        public string Name { get; set; }
    }
}

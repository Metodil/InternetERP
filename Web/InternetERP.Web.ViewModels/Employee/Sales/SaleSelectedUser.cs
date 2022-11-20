using AutoMapper;
using InternetERP.Data.Models;
using InternetERP.Services.Mapping;
using InternetERP.Web.ViewModels.Administration.Users;

namespace InternetERP.Web.ViewModels.Employee.Sales
{
    public class SaleSelectedUser : IMapTo<ApplicationUser>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ApplicationUser, SaleSelectedUser>()
                .ForMember(
                    m => m.Phone,
                    opt => opt.MapFrom(x => x.PhoneNumber))
                .ForMember(
                    m => m.Address,
                    opt => opt.MapFrom(x =>
                    x.Town.Name + ", " +
                    x.District + ", " +
                    x.Street));
        }
    }
}

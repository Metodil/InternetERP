namespace InternetERP.Services.Data.Tests
{
    using System.Reflection;

    using AutoMapper;
    using InternetERP.Services.Mapping;

    public class MapperInitializationProfile : Profile
    {
        public MapperInitializationProfile()
        {
            AutoMapperConfig.RegisterMappings(Assembly.GetCallingAssembly());
        }
    }
}

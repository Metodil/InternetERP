using InternetERP.Data.Common.Repositories;
using InternetERP.Data.Models;
using Moq;

namespace InternetERP.Services.Data.Tests
{
    public class TownsServiceTests
    {
        private readonly Mock<IDeletableEntityRepository<Town>> townsRepository;

        public TownsServiceTests()
        {
            this.townsRepository = new Mock<IDeletableEntityRepository<Town>>();
        }

        //[Fact]
    }
}

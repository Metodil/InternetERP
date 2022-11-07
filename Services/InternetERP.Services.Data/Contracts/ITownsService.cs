namespace InternetERP.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ITownsService
    {
        public Task<IEnumerable<KeyValuePair<string, string>>> GetAllTownsAsKetValuePairsAsync();
    }
}

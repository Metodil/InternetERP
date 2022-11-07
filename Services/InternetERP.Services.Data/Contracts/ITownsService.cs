namespace InternetERP.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ITownsService
    {
        public IEnumerable<KeyValuePair<string, string>> GetAllTownsAsKetValuePairs();
    }
}

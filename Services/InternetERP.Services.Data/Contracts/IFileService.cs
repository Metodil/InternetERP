namespace InternetERP.Services.Data.Contracts
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface IFileService
    {
        Task<bool> DeleteImageProduct(string imageId, string path);

        Task<bool> UploadFile(string fileName, string path, IFormFile file);
    }
}

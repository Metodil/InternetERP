namespace InternetERP.Services.Data.Employee
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    using InternetERP.Services.Data.Contracts;
    using Microsoft.AspNetCore.Http;

    public class FileLocalService : IFileService
    {
        public async Task<bool> UploadFile(string fileName, string path, IFormFile file)
        {
            try
            {
                if (file.Length > 0)
                {
                    using (var fileStream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }

                    return true;
                }
                else
                {
                    throw new Exception("File size is invalid");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("File Copy Failed", ex);
            }
        }

        public async Task<bool> DeleteImageProduct(string imageId, string path)
        {
            var result = false;
            var fileName = Path.Combine(path, imageId);
            if (await Task.Run(() => File.Exists(fileName)))
            {
                File.Delete(fileName);
                result = true;
            }

            return result;
        }

        // TODO ChechDirrectoryExist as common service
        private void ChechDirrectoryExist(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}

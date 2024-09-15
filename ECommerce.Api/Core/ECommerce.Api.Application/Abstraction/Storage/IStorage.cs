using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Api.Application.Abstraction.Storage
{
    public interface IStorage
    {
        Task<List<(string filename, string path)>> UploadAsync(string pathOrContainerName, IFormFileCollection files);
        Task DeleteAsync(string pathOrContainerName, string fileName);
        List<string> GetFiles(string pathOrContainerName);
        bool HasFile(string pathOrContainerName, string fileName);

        //Task<List<(string filename, string path)>> UploadAsync(string path, IFormFileCollection files);
        //Task<bool> CopyFileAsync(string path, IFormFile file);
    }
}

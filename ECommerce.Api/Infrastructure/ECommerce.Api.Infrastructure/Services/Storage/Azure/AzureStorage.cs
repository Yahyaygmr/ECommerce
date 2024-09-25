using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using ECommerce.Api.Application.Abstraction.Storage.Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Api.Infrastructure.Services.Storage.Azure
{
    public class AzureStorage : IAzureStorage
    {
        private readonly BlobServiceClient _blobServiceClient;
        BlobContainerClient _blobContainerClient;
        public AzureStorage(IConfiguration configuration)
        {
            _blobServiceClient = new(configuration["Storage:Azure"]);
        }
        public async Task DeleteAsync(string pathOrContainerName, string fileName)
        {
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(pathOrContainerName);
            BlobClient blobClient = _blobContainerClient.GetBlobClient(fileName);
            await blobClient.DeleteAsync();

        }

        public List<string> GetFiles(string pathOrContainerName)
        {
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(pathOrContainerName);
            return _blobContainerClient.GetBlobs().Select(x => x.Name).ToList();
        }

        public bool HasFile(string pathOrContainerName, string fileName)
        {
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(pathOrContainerName);
            return _blobContainerClient.GetBlobs().Any(x => x.Name == fileName);
        }

        public async Task<List<(string filename, string path)>> UploadAsync(string pathOrContainerName, IFormFileCollection files)
        {
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(pathOrContainerName);
            await _blobContainerClient.CreateIfNotExistsAsync();
            await _blobContainerClient.SetAccessPolicyAsync(PublicAccessType.BlobContainer);

            List<(string filename, string path)> datas = new();
            foreach (IFormFile file in files)
            {
                BlobClient blobClient = _blobContainerClient.GetBlobClient(file.Name);
                await blobClient.UploadAsync(file.OpenReadStream());
                datas.Add((file.Name, pathOrContainerName));
            }
            return datas;
        }
    }
}

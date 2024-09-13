using ECommerce.Api.Application.Services;
using ECommerce.Api.Infrastructure.Operations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Api.Infrastructure.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<bool> CopyFileAsync(string path, IFormFile file)
        {
            try
            {
                await using FileStream fileStream = new(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);
                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
                return true;
            }
            catch (Exception ex)
            {

                //todo log!
                throw ex;
            }
        }

        async Task<string> FileRenameAsync(string path, string fileName)
        {

            string extension = Path.GetExtension(fileName);
            string oldName = Path.GetFileNameWithoutExtension(fileName);
            string newFileName = $"{NameOperation.CharacterRegulatory(oldName)}{extension}";

            if (File.Exists($"{path}\\{newFileName}"))
            {
                int i = 1;
                while (File.Exists($"{path}\\{newFileName}"))
                {
                    string tempNewFileName = $"{NameOperation.CharacterRegulatory(oldName)}({i}){extension}";
                    newFileName = tempNewFileName;
                    i++;
                }

                return newFileName;
            }
            else
            {
                return newFileName;
            }
        }

        public async Task<List<(string filename, string path)>> UploadAsync(string path, IFormFileCollection files)
        {
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, path);

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);
            List<(string filename, string path)> datas = new();
            List<bool> results = new();

            foreach (IFormFile file in files)
            {
                string fileNewName = await FileRenameAsync(uploadPath, file.FileName);
                bool result = await CopyFileAsync($"{uploadPath}\\{fileNewName}", file);
                datas.Add((fileNewName, $"{path}\\{fileNewName}"));
                results.Add(result);
            }

            if (results.TrueForAll(result => result.Equals(true)))
            {
                return datas;
            }

            //todo hata alındığına dair ex fırlatılması gerekiyor.

            return null;
        }
    }
}


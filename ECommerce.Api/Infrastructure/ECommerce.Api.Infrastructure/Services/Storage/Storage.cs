using ECommerce.Api.Infrastructure.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Api.Infrastructure.Services.Storage
{
    public class Storage
    {
        protected delegate bool HasFile(string pathOrContainerName, string fileName);
        protected async Task<string> FileRenameAsync(string pathOrContainerName, string fileName, HasFile hasFile)
        {
            string extension = Path.GetExtension(fileName);
            string oldName = Path.GetFileNameWithoutExtension(fileName);
            string newFileName = $"{NameOperation.CharacterRegulatory(oldName)}{extension}";
            // File.Exists($"{path}\\{newFileName}")
            if (hasFile(pathOrContainerName, newFileName))
            {
                int i = 1;
                while (hasFile(pathOrContainerName, newFileName))
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

    }
}

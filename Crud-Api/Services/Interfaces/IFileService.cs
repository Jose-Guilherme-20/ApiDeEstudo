using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Crud_Api.Model;
using Microsoft.AspNetCore.Http;

namespace Crud_Api.Services.Interfaces
{
    public interface IFileService
    {
        byte[] GetFile(string filename);
        Task<FileDocument> SaveFileToDisk(IFormFile file);
        Task<List<FileDocument>> SaveFilesToDisk(IList<IFormFile> file);
    }
}
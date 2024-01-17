using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Crud_Api.Model;
using Crud_Api.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Crud_Api.Services
{
    public class FileService : IFileService
    {
        private readonly string _basePath;
        private readonly IHttpContextAccessor _context;

        public FileService(IHttpContextAccessor context)
        {
            _context = context;
            _basePath = Directory.GetCurrentDirectory() + "\\UploadDir\\";
        }

        public byte[] GetFile(string filename)
        {
            var filePath = _basePath + filename;
            return File.ReadAllBytes(filePath);
        }

        public async Task<List<FileDocument>> SaveFilesToDisk(IList<IFormFile> files)
        {
            var filesList = new List<FileDocument>();
            foreach (var file in files)
            {
                filesList.Add(await SaveFileToDisk(file));
            }

            return filesList;
        }

        public async Task<FileDocument> SaveFileToDisk(IFormFile file)
        {
            FileDocument fileDocument = new FileDocument();

            var fileType = Path.GetExtension(file.FileName);
            var baseUrl = _context.HttpContext.Request.Host;

            if (fileType.ToLower() == ".pdf" || fileType.ToLower() == ".jpg" ||
            fileType.ToLower() == ".png" || fileType.ToLower() == ".jpeg")
            {
                var docName = Path.GetFileName(file.FileName);
                if (file != null && file.Length > 0)
                {
                    var destination = Path.Combine(_basePath, "", docName);
                    fileDocument.DocumentName = docName;
                    fileDocument.DocType = fileType;
                    fileDocument.DocUrl = Path.Combine(baseUrl + "/api/file/v1" + fileDocument.DocumentName);

                    using var stream = new FileStream(destination, FileMode.Create);
                    await file.CopyToAsync(stream);
                }
            }
            return fileDocument;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Crud_Api.Services;
using Crud_Api.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Crud_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/v1/")]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;
        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }


        [HttpGet("downloadFile/{fileName}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType((200), Type = typeof(byte[]))]
        [Produces("application/octet-stream")]

        public async Task<IActionResult> GetFileAsync(string fileName)
        {
            byte[] buffer = _fileService.GetFile(fileName);
            if (buffer != null)
            {
                HttpContext.Response.ContentType = $"application/{Path.GetExtension(fileName).Replace(".", "")}";
                HttpContext.Response.Headers.Add("content-length", buffer.Length.ToString());
                await HttpContext.Response.Body.WriteAsync(buffer, 0, buffer.Length);
            }
            return new ContentResult();
        }

        [HttpPost("uploadFile")]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(200)]

        public async Task<IActionResult> UploadFile([FromForm] IFormFile file)
        {
            var request = await _fileService.SaveFileToDisk(file);
            return Ok(request);
        }

        [HttpPost("uploadMultipleFile")]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(200)]

        public async Task<IActionResult> UploadManyFile([FromForm] List<IFormFile> files)
        {
            var request = await _fileService.SaveFilesToDisk(files);
            return Ok(request);
        }
    }
}
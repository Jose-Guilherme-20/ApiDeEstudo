using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crud_Api.Model;
using Crud_Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Crud_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/v1")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }
        [HttpPost]
        public IActionResult Create([FromBody] Book book)
        {
            _bookService.Create(book);
            return Ok(book);
        }
    }
}
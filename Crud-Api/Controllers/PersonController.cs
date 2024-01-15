using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asp.Versioning;
using Crud_Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using Services.Interfaces;

namespace Crud_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/v1/")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly ITokenService _tokenService;
        public PersonController(IPersonService personService, ITokenService tokenService)
        {
            _personService = personService;
            _tokenService = tokenService;
        }
        [HttpGet]
        [ProducesResponseType((200), Type = typeof(Person))]
        [ProducesResponseType((204))]
        [ProducesResponseType((400))]
        [ProducesResponseType((500))]

        public ActionResult<List<Person>> GetAll()
        {
            return Ok(_personService.FindAll());
        }
        [HttpPost]
        public ActionResult<Person> Create([FromBody] Person person)
        {
            _personService.Create(person);
            return Ok(person);
        }
        [HttpPost("login")]
        public ActionResult<string> PersonLogin([FromBody] Person person)
        {
            var token = _tokenService.GenerateToken(person);
            return Ok(token);
        }
        [HttpGet("findByName")]
        public ActionResult<List<Person>> FindByName([FromQuery] string name, [FromQuery] string lastName)
        {
            if (string.IsNullOrWhiteSpace(name) && string.IsNullOrWhiteSpace(lastName))
            {
                return BadRequest();
            }
            return Ok(_personService.FindByName(name, lastName));
        }

        [HttpGet("{sortDirection}/{pageSize}/{page}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]

        public IActionResult Get([FromQuery] string name, string sortDirection, int pageSize, int page)
        {
            return Ok(_personService.FindWithPagedSearch(name, sortDirection, pageSize, page));
        }
    }
}
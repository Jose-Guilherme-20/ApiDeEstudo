using Crud_Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace Crud_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/v1/")]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        public AuthController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }
        [HttpPost("signin")]
        public ActionResult<string> GenereteToken([FromBody] Person person)
        {
            var result = _tokenService.GenerateToken(person);
            return Ok(result);
        }
    }
}
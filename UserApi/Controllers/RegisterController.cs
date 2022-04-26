using FluentResults;
using Microsoft.AspNetCore.Mvc;
using UserApi.Data.Dtos;
using UserApi.Data.Requests;
using UserApi.Services;

namespace UserApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private RegisterService _registerService;

        public RegisterController(RegisterService registerService)
        {
            _registerService = registerService;
        }

        [HttpPost]
        public IActionResult RegisterUser(CreateUserDto createDto)
        {
            Result result = _registerService.CreateUser(createDto);

            if (result.IsFailed) 
                return StatusCode(500);

            return Ok(result.Successes.FirstOrDefault());
        }

        [HttpGet("/confirm-email")]
        public IActionResult confirmEmail([FromQuery] ConfirmEmailRequest request)
        {
            Result result = _registerService.ConfirmUser(request);

            if (result.IsFailed)
                return StatusCode(500);

            return Ok(result.Successes.FirstOrDefault());
        }
    }
}

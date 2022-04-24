using FluentResults;
using Microsoft.AspNetCore.Mvc;
using UserApi.Data.Requests;
using UserApi.Services;

namespace UserApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private LoginService _loginService;

        public LoginController(LoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public IActionResult Login(LoginRequest request)
        {
            Result result = _loginService.Login(request);

            if (result.IsFailed)
                return Unauthorized(result.Errors.FirstOrDefault());

            return Ok(result.Successes.FirstOrDefault());
        }
    }
}
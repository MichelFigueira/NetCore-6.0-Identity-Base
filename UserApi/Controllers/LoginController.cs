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

        [HttpPost("/reset-password")]
        public IActionResult ResetPassword(ResetPasswordRequest request)
        {
            Result result = _loginService.ResetPassword(request);

            if (result.IsFailed)
                return Unauthorized(result.Errors.FirstOrDefault());

            return Ok(result.Successes.FirstOrDefault());
        }

        [HttpPost("/confirm-reset-password")]
        public IActionResult ResetPassword(ConfirmResetPasswordRequest request)
        {
            Result result = _loginService.ConfirmResetPassword(request);

            if (result.IsFailed)
                return Unauthorized(result.Errors.FirstOrDefault());

            return Ok(result.Successes.FirstOrDefault());
        }
    }
}
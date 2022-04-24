using FluentResults;
using Microsoft.AspNetCore.Mvc;
using UserApi.Services;

namespace UserApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LogoutController : ControllerBase
    {
        private LogoutService _logoutService;

        public LogoutController(LogoutService logoutService)
        {
            _logoutService = logoutService;
        }

        [HttpPost]
        public IActionResult Logout()
        {
            Result result = _logoutService.Logout();

            if (result.IsFailed) 
                return Unauthorized(result.Errors.FirstOrDefault());

            return Ok(result.Successes.FirstOrDefault());
        }
    }

}

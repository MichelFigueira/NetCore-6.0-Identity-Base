using FluentResults;
using Microsoft.AspNetCore.Identity;
using UserApi.Models;

namespace UserApi.Services
{
    public class LogoutService
    {
        private SignInManager<CustomIdentityUser> _signInManager;

        public LogoutService(SignInManager<CustomIdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public Result Logout()
        {
            var resultIdentity = _signInManager.SignOutAsync();

            if (resultIdentity.IsCompletedSuccessfully)
                return Result.Ok();

            return Result.Fail("Error to Logout");
        }
    }
}

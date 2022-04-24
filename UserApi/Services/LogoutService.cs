using FluentResults;
using Microsoft.AspNetCore.Identity;

namespace UserApi.Services
{
    public class LogoutService
    {
        private SignInManager<IdentityUser<int>> _signInManager;

        public LogoutService(SignInManager<IdentityUser<int>> signInManager)
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

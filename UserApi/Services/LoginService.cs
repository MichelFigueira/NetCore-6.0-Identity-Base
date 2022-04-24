using FluentResults;
using Microsoft.AspNetCore.Identity;
using UserApi.Data.Requests;
using UserApi.Models;

namespace UserApi.Services
{
    public class LoginService
    {
        private SignInManager<IdentityUser<int>> _signInManager;
        private TokenService _tokenService;

        public LoginService(SignInManager<IdentityUser<int>> signInManager, TokenService tokenService)
        {
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public Result Login(LoginRequest request)
        {
            var resultIdentity = _signInManager.PasswordSignInAsync(request.Username, request.Password, false, false);

            if (resultIdentity.Result.Succeeded)
            {
                var identityUser = _signInManager
                    .UserManager
                    .Users
                    .FirstOrDefault(usr =>
                    usr.NormalizedUserName == request.Username.ToUpper());

                Token token = _tokenService.CreateToken(identityUser);

                return Result.Ok().WithSuccess(token.Value);
            }

            return Result.Fail("Login Error");

        }
    }
}

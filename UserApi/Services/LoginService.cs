using FluentResults;
using Microsoft.AspNetCore.Identity;
using UserApi.Data.Requests;
using UserApi.Models;

namespace UserApi.Services
{
    public class LoginService
    {
        private SignInManager<CustomIdentityUser> _signInManager;
        private TokenService _tokenService;

        public LoginService(SignInManager<CustomIdentityUser> signInManager, TokenService tokenService)
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

                Token token = _tokenService
                    .CreateToken(identityUser, _signInManager
                        .UserManager.GetRolesAsync(identityUser).Result.FirstOrDefault());

                return Result.Ok().WithSuccess(token.Value);
            }

            return Result.Fail("Login Error");

        }

        public Result ResetPassword(ResetPasswordRequest request)
        {
            CustomIdentityUser identityUser = RecoverUserByEmail(request.Email);

            if (identityUser != null)
            {
                string codeReset = _signInManager
                    .UserManager.GeneratePasswordResetTokenAsync(identityUser).Result;

                return Result.Ok().WithSuccess(codeReset);
            }

            return Result.Fail("Reset Email Failed");

        }

        public Result ConfirmResetPassword(ConfirmResetPasswordRequest request)
        {
            CustomIdentityUser identityUser = RecoverUserByEmail(request.Email);

            IdentityResult identityResult = _signInManager
                .UserManager.ResetPasswordAsync(identityUser, request.Token, request.Password)
                .Result;

            if (identityResult.Succeeded)
                return Result.Ok().WithSuccess("Password reset successfully");

            return Result.Fail("Error");

        }

        private CustomIdentityUser RecoverUserByEmail(string email)
        {
            return _signInManager
                        .UserManager
                        .Users
                        .FirstOrDefault(u => u.NormalizedEmail == email.ToUpper());
        }
    }
}

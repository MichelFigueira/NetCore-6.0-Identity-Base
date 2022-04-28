using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using System.Web;
using UserApi.Data.Dtos;
using UserApi.Data.Requests;
using UserApi.Models;

namespace UserApi.Services
{
    public class RegisterService
    {
        private IMapper _mapper;
        private UserManager<IdentityUser<int>> _userManager;
        private EmailService _mailService;

        public RegisterService(IMapper mapper, UserManager<IdentityUser<int>> userManager, EmailService mailService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _mailService = mailService;
        }

        public Result CreateUser(CreateUserDto createDto)
        {
            User user = _mapper.Map<User>(createDto);
            IdentityUser<int> userIdentity = _mapper.Map<IdentityUser<int>>(user);

            Task<IdentityResult> resultIdentity = _userManager.CreateAsync(userIdentity, createDto.Password);

            _userManager.AddToRoleAsync(userIdentity, "regular");

            if (resultIdentity.Result.Succeeded) 
            { 
                var code = _userManager.GenerateEmailConfirmationTokenAsync(userIdentity).Result;
                var encodedCode = HttpUtility.UrlEncode(code);

                _mailService.SendEmail(new[] { userIdentity.Email }, "Email Confirmation", userIdentity.Id, encodedCode);

                return Result.Ok().WithSuccess(code); 
            }

            return Result.Fail("Fail to register!");
        }

        public Result ConfirmUser(ConfirmEmailRequest request)
        {
            var identityUser = _userManager.Users
                .FirstOrDefault(usr => usr.Id == request.UserId);

            var identityResult = _userManager.ConfirmEmailAsync(identityUser, request.ConfirmationToken).Result;

            if (identityResult.Succeeded)
                return Result.Ok();

            return Result.Fail("Failed to activate user account");
        }

    }
}

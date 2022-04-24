using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using UserApi.Data.Dtos;
using UserApi.Models;

namespace UserApi.Services
{
    public class RegisterService
    {
        private IMapper _mapper;
        private UserManager<IdentityUser<int>> _userManager;

        public RegisterService(IMapper mapper, UserManager<IdentityUser<int>> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        public Result CreateUser(CreateUserDto createDto)
        {
            User user = _mapper.Map<User>(createDto);
            IdentityUser<int> userIdentity = _mapper.Map<IdentityUser<int>>(user);

            Task<IdentityResult> resultIdentity = _userManager.CreateAsync(userIdentity, createDto.Password);

            if (resultIdentity.Result.Succeeded) 
                return Result.Ok();

            return Result.Fail("Fail to register!");
        }
    }
}

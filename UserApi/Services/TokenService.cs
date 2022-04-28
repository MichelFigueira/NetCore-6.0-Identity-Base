using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserApi.Models;

namespace UserApi.Services
{
    public class TokenService
    {
        public Token CreateToken(CustomIdentityUser user, string role)
        {
            Claim[] claimUser = new Claim[]
            {
                new Claim("username", user.UserName),
                new Claim("id", user.Id.ToString()),
                new Claim(ClaimTypes.Role, role),
                new Claim(ClaimTypes.DateOfBirth, user.DateOfBirth.ToString())
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("kKE434gge32!jgritr9452323#@494fkfhjbvpppqxlfredvr$#")
                );
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claimUser,
                signingCredentials: credentials,
                expires: DateTime.UtcNow.AddHours(6)
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return new Token(tokenString);
        }
    }
}

using Microsoft.AspNetCore.Identity;

namespace UserApi.Models
{
    public class CustomIdentityUser : IdentityUser<int>
    {
        public DateTime DateOfBirth { get; set; }
    }
}

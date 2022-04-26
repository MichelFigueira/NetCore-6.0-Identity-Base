using System.ComponentModel.DataAnnotations;

namespace UserApi.Data.Requests
{
    public class ResetPasswordRequest
    {
        [Required]
        public string Email { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace UserApi.Data.Requests
{
    public class ConfirmEmailRequest
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string ConfirmationToken { get; set; }
    }
}

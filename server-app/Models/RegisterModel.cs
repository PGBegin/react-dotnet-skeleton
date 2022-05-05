using System.ComponentModel.DataAnnotations;

namespace server_app.Models
{
    public class RegisterModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set;}
        [Required]
        [RegularExpression("^[a-zA-Z0-9.?/-]{8,24}$", ErrorMessage ="Password must be complex")]
        public string Password {get; set; }
        [Required]
        public string Username { get; set; }

        
    }
}
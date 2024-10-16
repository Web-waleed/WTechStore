using System.ComponentModel.DataAnnotations;

namespace WTechStore.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public String? Email { get; set; }
        [DataType(DataType.Password)]
        [Required]
        public String? Password { get; set; }
         public bool RememberMe { get; set; }
    }
}

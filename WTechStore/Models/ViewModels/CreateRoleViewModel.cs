using System.ComponentModel.DataAnnotations;

namespace WTechStore.Models.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required]
        public String RoleName {  get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace WTechStore.Models.ViewModels
{
	public class RegisterViewModel
	{
		[Required]
		public String FirstName { get; set; }
		[Required]
		public String LastName { get; set; }
		[Required]
		[EmailAddress]
		public String Email { get; set; }
		[Required]
		public String PhoneNumber { get; set; }
		[DataType(DataType.Password)]
		[Required]
		public String Password { get; set; }
		[Required]
		[DataType(DataType.Password)]
		[Compare("Password",ErrorMessage="PASSWORD NOT MATCH")]
		public String ConfirmPassword {  get; set; }

	}
}

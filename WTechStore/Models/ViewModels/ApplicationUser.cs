using Microsoft.AspNetCore.Identity;
using System.Drawing.Printing;

namespace WTechStore.Models.ViewModels
{
	public class ApplicationUser : IdentityUser
	{
		public String FirstName {  get; set; }
		public String LastName {  get; set; }
	}
}

using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Portal.CMS.Entities.Entities.Models
{
	public class ApplicationRole : IdentityRole
	{
		public ApplicationRole() { }
		public ApplicationRole(string name) { Name = name; }

		[Required]
		[DefaultValue("true")]
		public bool IsAssignable { get; set; }
	}
}
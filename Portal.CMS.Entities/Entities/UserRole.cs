using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portal.CMS.Entities.Entities
{
	public class UserRole
	{
		[ForeignKey("User")]
		[Key, Column(Order = 1)]
		public int UserId { get; set; }

		[ForeignKey("Role")]
		[Key, Column(Order = 2)]
		public int RoleId { get; set; }

		public virtual User User { get; set; }

		public virtual Role Role { get; set; }
	}
}
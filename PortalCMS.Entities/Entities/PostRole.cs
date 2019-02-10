using PortalCMS.Entities.Entities.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalCMS.Entities.Entities
{
	public class PostRole
	{
		[Key]
		public int PostRoleId { get; set; }

		[ForeignKey("Post")]
		public int PostId { get; set; }

		public virtual Post Post { get; set; }

		[ForeignKey("Role")]
		public string RoleId { get; set; }

		public virtual ApplicationRole Role { get; set; }
	}
}
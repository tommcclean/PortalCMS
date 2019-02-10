using PortalCMS.Entities.Entities.Models;
using PortalCMS.Entities.Enumerators;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalCMS.Entities.Entities
{
	public class UserToken
	{
		[Key]
		public int UserTokenId { get; set; }

		[Required]
		public UserTokenType UserTokenType { get; set; }

		[Required]
		public string Token { get; set; }

		[ForeignKey("User")]
		public string UserId { get; set; }

		[Required]
		public DateTime DateAdded { get; set; }

		public DateTime? DateRedeemed { get; set; }

		public virtual ApplicationUser User { get; set; }
	}
}
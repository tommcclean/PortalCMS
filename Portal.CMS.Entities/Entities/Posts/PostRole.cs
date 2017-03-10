using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Portal.CMS.Entities.Entities.Authentication;

namespace Portal.CMS.Entities.Entities.Posts
{
    public class PostRole
    {
        [Key]
        public int PostRoleId { get; set; }

        [ForeignKey("Post")]
        public int PostId { get; set; }

        public virtual Post Post { get; set; }

        [ForeignKey("Role")]
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
    }
}
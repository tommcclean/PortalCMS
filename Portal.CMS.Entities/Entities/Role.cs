using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Portal.CMS.Entities.Entities
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }

        [Required]
        public string RoleName { get; set; }

        [Required]
        [DefaultValue("true")]
        public bool IsAssignable { get; set; }
    }
}
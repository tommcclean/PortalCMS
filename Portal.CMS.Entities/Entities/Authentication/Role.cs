using System.ComponentModel.DataAnnotations;

namespace Portal.CMS.Entities.Entities.Authentication
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }

        [Required]
        public string RoleName { get; set; }
    }
}
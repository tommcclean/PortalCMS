using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Portal.CMS.Entities.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string EmailAddress { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string GivenName { get; set; }

        [Required]
        public string FamilyName { get; set; }

        [Required]
        public DateTime DateAdded { get; set; }

        [Required]
        public DateTime DateUpdated { get; set; }

        public string AvatarImagePath { get; set; }

        public string Bio { get; set; }

        public virtual ICollection<UserRole> Roles { get; set; }
    }
}
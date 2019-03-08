﻿using Microsoft.AspNet.Identity.EntityFramework;
using PortalCMS.Entities.Entities;
using PortalCMS.Entities.Entities.Models;
using PortalCMS.Entities.Enumerators;
using PortalCMS.Web.ViewModels.Shared;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PortalCMS.Web.Areas.Admin.ViewModels.BlogManager
{
    public class CreatePostViewModel
    {
        [DisplayName("Headline")]
        [Required]
        public string PostTitle { get; set; }

        [DisplayName("Description")]
        [Required]
        public string PostDescription { get; set; }

        [DisplayName("Body")]
        [Required]
        public string PostBody { get; set; }

        [DisplayName("Category")]
        [Required]
        public int PostCategoryId { get; set; }

        [DisplayName("Author")]
        [Required]
        public string PostAuthorUserId { get; set; }

        [DisplayName("Status")]
        public PublicationState PublicationState { get; set; }

        [DisplayName("Roles")]
        public List<string> SelectedRoleList { get; set; } = new List<string>();

        public PaginationViewModel BannerImages { get; set; }

        public PaginationViewModel GalleryImages { get; set; }

        #region Hidden Fields

        public int BannerImageId { get; set; }

        public string GalleryImageList { get; set; }

        #endregion Hidden Fields

        #region Enumerable Properties

        public IEnumerable<PostCategory> PostCategoryList { get; set; }

        public IEnumerable<ApplicationUser> UserList { get; set; }

        public List<ApplicationRole> RoleList { get; set; }

        #endregion Enumerable Properties
    }
}
namespace Portal.CMS.Entities
{
    using Entities.Analytics;
    using Entities.Authentication;
    using Entities.Copy;
    using Entities.Generic;
    using Entities.Menu;
    using Entities.PageBuilder;
    using Entities.Posts;
    using Entities.Settings;
    using Entities.Themes;
    using System.Data.Common;
    using System.Data.Entity;

    public class PortalEntityModel : DbContext
    {
        public PortalEntityModel(DbConnection connection) : base(connection, true)
        {
        }

        public PortalEntityModel() : base("name=PortalEntityModel")
        {
        }

        #region Authentication Entities

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Role> Roles { get; set; }

        public virtual DbSet<UserRole> UserRoles { get; set; }

        public virtual DbSet<UserToken> UserTokens { get; set; }

        #endregion Authentication Entities

        #region Analytic Entities

        public DbSet<AnalyticPageView> AnalyticPageViews { get; set; }

        public DbSet<AnalyticPostView> AnalyticPostViews { get; set; }

        #endregion Analytic Entities

        #region Post Entities

        public virtual DbSet<Post> Posts { get; set; }

        public virtual DbSet<PostCategory> PostCategories { get; set; }

        public virtual DbSet<PostImage> PostImages { get; set; }

        public virtual DbSet<PostComment> PostComments { get; set; }

        public virtual DbSet<PostRole> PostRoles { get; set; }

        #endregion Post Entities

        #region Generic Entities

        public virtual DbSet<Image> Images { get; set; }

        #endregion Generic Entities

        #region Copy Entities

        public virtual DbSet<Copy> CopySections { get; set; }

        #endregion Copy Entities

        #region Menu Entities

        public virtual DbSet<Menu> Menus { get; set; }

        public virtual DbSet<MenuItem> MenuItems { get; set; }

        public virtual DbSet<MenuItemRole> MenuItemRoles { get; set; }

        #endregion Menu Entities

        #region Setting Entities

        public virtual DbSet<Setting> Settings { get; set; }

        #endregion Setting Entities

        #region Page Builder Entities

        public virtual DbSet<Page> Pages { get; set; }

        public virtual DbSet<PageSection> PageSections { get; set; }

        public virtual DbSet<PageSectionType> PageSectionTypes { get; set; }

        public virtual DbSet<PageComponentType> PageComponentTypes { get; set; }

        public virtual DbSet<PageRole> PageRoles { get; set; }

        public virtual DbSet<PageSectionRole> PageSectionRoles { get; set; }

        #endregion Page Builder Entities

        #region Theme Entities

        public virtual DbSet<Font> Fonts { get; set; }

        public virtual DbSet<Theme> Themes { get; set; }

        #endregion Theme Entities
    }
}
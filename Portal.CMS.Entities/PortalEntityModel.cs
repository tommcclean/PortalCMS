using Portal.CMS.Entities.Entities;
using System.Data.Common;
using System.Data.Entity;

namespace Portal.CMS.Entities
{
    public class PortalEntityModel : DbContext
    {
        #region Dependencies

        public PortalEntityModel(DbConnection connection) : base(connection, true)
        {
        }

        public PortalEntityModel() : base("name=PortalEntityModel")
        {
        }

        #endregion Dependencies

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Role> Roles { get; set; }

        public virtual DbSet<UserRole> UserRoles { get; set; }

        public virtual DbSet<UserToken> UserTokens { get; set; }

        public virtual DbSet<AnalyticPageView> AnalyticPageViews { get; set; }

        public virtual DbSet<AnalyticPostView> AnalyticPostViews { get; set; }

        public virtual DbSet<Post> Posts { get; set; }

        public virtual DbSet<PostCategory> PostCategories { get; set; }

        public virtual DbSet<PostImage> PostImages { get; set; }

        public virtual DbSet<PostComment> PostComments { get; set; }

        public virtual DbSet<PostRole> PostRoles { get; set; }

        public virtual DbSet<Image> Images { get; set; }

        public virtual DbSet<CopyItem> CopyItems { get; set; }

        public virtual DbSet<MenuSystem> Menus { get; set; }

        public virtual DbSet<MenuItem> MenuItems { get; set; }

        public virtual DbSet<MenuItemRole> MenuItemRoles { get; set; }

        public virtual DbSet<Setting> Settings { get; set; }

        public virtual DbSet<Page> Pages { get; set; }

        public virtual DbSet<PageAssociation> PageAssociations { get; set; }

        public virtual DbSet<PageSection> PageSections { get; set; }

        public virtual DbSet<PagePartial> PagePartials { get; set; }

        public virtual DbSet<PageSectionBackup> PageSectionBackups { get; set; }

        public virtual DbSet<PageSectionType> PageSectionTypes { get; set; }

        public virtual DbSet<PageComponentType> PageComponentTypes { get; set; }

        public virtual DbSet<PageRole> PageRoles { get; set; }

        public virtual DbSet<PageAssociationRole> PageAssociationRoles { get; set; }

        public virtual DbSet<Font> Fonts { get; set; }

        public virtual DbSet<CustomTheme> Themes { get; set; }
    }
}
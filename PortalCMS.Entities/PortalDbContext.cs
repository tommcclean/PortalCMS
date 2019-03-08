using Microsoft.AspNet.Identity.EntityFramework;
using PortalCMS.Entities.Entities;
using PortalCMS.Entities.Entities.Models;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace PortalCMS.Entities
{
	public class PortalDbContext : IdentityDbContext<ApplicationUser>
	{
		#region Dependencies

		public PortalDbContext(DbConnection connection) : base(connection, true)
		{
			Database.SetInitializer<PortalDbContext>(null);
		}

		public PortalDbContext() : base("name=PortalDbConnection")
		{
		}

		#endregion Dependencies

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

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

			#region Rename identity tables

			// Add this - so that IdentityUser can share a table with ApplicationUser
			modelBuilder.Entity<IdentityUser>().ToTable("User");
			modelBuilder.Entity<ApplicationUser>().ToTable("User");

			// Add this - so that IdentityRole can share a table with ApplicationRole
			modelBuilder.Entity<IdentityRole>().ToTable("UserRole");
			modelBuilder.Entity<ApplicationRole>().ToTable("UserRole");

			modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles");
			modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogin");
			modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaim");

			#endregion
		}

		public static PortalDbContext Create()
		{
			return new PortalDbContext();
		}
	}
}
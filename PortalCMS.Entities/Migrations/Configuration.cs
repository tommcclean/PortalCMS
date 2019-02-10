namespace PortalCMS.Entities.Migrations
{
    using Seed;
    using System.Data.Entity.Migrations;

	internal sealed class Configuration : DbMigrationsConfiguration<PortalCMS.Entities.PortalDbContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = true;
		}

		protected override void Seed(PortalCMS.Entities.PortalDbContext context)
		{
			ThemeSeed.Seed(context);
			context.SaveChanges();

			RoleSeed.Seed(context);
			context.SaveChanges();

			SettingSeed.Seed(context);
			context.SaveChanges();

			PostCategorySeed.Seed(context);
			context.SaveChanges();

			PageSectionTypeSeed.Seed(context);
			context.SaveChanges();

			PageComponentTypeSeed.Seed(context);
			context.SaveChanges();

			MediaSeed.Seed(context);
			context.SaveChanges();

			CopySeed.Seed(context);
			context.SaveChanges();

			MenuSeed.Seed(context);
			context.SaveChanges();

			PageSeed.Seed(context);
			context.SaveChanges();

			//PostSeed.Seed(context);
			//context.SaveChanges();
		}
	}
}
namespace Portal.CMS.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public partial class UpdateExistingAdminAndEditorRoles : DbMigration
    {
        private readonly PortalEntityModel _context = new PortalEntityModel();

        public override void Up()
        {
            var roleList = _context.Roles.ToList();

            var adminRole = roleList.FirstOrDefault(x => "Admin".Equals(x.RoleName, StringComparison.OrdinalIgnoreCase));
            if (adminRole != null) adminRole.IsAssignable = true;

            var editorRole = roleList.FirstOrDefault(x => "Editor".Equals(x.RoleName, StringComparison.OrdinalIgnoreCase));
            if (editorRole != null) editorRole.IsAssignable = true;

            _context.SaveChanges();
        }

        public override void Down()
        {
        }
    }
}
using Microsoft.AspNet.Identity.EntityFramework;
using Portal.CMS.Entities;
using Portal.CMS.Entities.Entities.Models;
using Portal.CMS.Repositories.Base;

namespace Portal.CMS.Services
{
	public abstract class ServiceBase<TObject>: RepositoryBase<TObject> where TObject : class
	{
		protected CustomUserManager UserManager;
		protected CustomRoleManager RoleManager;

		public ServiceBase() : this(new PortalDbContext()){}
    public ServiceBase(PortalDbContext context):base(context)
		{
			UserManager = new CustomUserManager(new CustomUserStore(context));
			RoleManager = new CustomRoleManager(new RoleStore<IdentityRole>(context));
		}
	}
}
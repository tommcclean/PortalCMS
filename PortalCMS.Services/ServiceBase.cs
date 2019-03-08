using Microsoft.AspNet.Identity.EntityFramework;
using PortalCMS.Entities;
using PortalCMS.Entities.Entities.Models;
using PortalCMS.Repositories.Base;

namespace PortalCMS.Services
{
	public abstract class ServiceBase<TObject>: RepositoryBase<TObject> where TObject : class
	{
		protected ApplicationUserManager UserManager;
		protected ApplicationRoleManager RoleManager;

		public ServiceBase() : this(new PortalDbContext()){}
    public ServiceBase(PortalDbContext context):base(context)
		{
			UserManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
			RoleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(context));
		}
	}
}
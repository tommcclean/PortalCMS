using Portal.CMS.Entities;
using Portal.CMS.Entities.Entities.Models;
using Portal.CMS.Repositories.Base;

namespace PortalCMS.Repositories
{
	public class UserRepository :RepositoryBase<CustomUser>
	{
		public UserRepository() : this(new PortalDbContext()) { }
		public UserRepository(PortalDbContext context) : base(context) { }
	}
}
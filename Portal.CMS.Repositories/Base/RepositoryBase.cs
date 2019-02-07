using Portal.CMS.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Portal.CMS.Repositories.Base
{
	public abstract class RepositoryBase<TObject> : IRepositoryBase<TObject> where TObject : class
	{
		protected PortalDbContext DbContext;

		public RepositoryBase(PortalDbContext context)
		{
			DbContext = context;
		}

		public ICollection<TObject> GetAll()
		{
			return DbContext.Set<TObject>().ToList();
		}

		public async Task<ICollection<TObject>> GetAllAsync()
		{
			return await DbContext.Set<TObject>().ToListAsync();
		}

		public TObject Get(object id)
		{
			return DbContext.Set<TObject>().Find(id);
		}

		public async Task<TObject> GetAsync(object id)
		{
			return await DbContext.Set<TObject>().FindAsync(id);
		}

		public TObject Find(Expression<Func<TObject, bool>> match)
		{
			return DbContext.Set<TObject>().SingleOrDefault(match);
		}

		public async Task<TObject> FindAsync(Expression<Func<TObject, bool>> match)
		{
			return await DbContext.Set<TObject>().SingleOrDefaultAsync(match);
		}

		public ICollection<TObject> FindAll(Expression<Func<TObject, bool>> match)
		{
			return DbContext.Set<TObject>().Where(match).ToList();
		}

		public async Task<ICollection<TObject>> FindAllAsync(Expression<Func<TObject, bool>> match)
		{
			return await DbContext.Set<TObject>().Where(match).ToListAsync();
		}

		public TObject Add(TObject t)
		{
			DbContext.Set<TObject>().Add(t);
			DbContext.SaveChanges();
			return t;
		}

		public async Task<TObject> AddAsync(TObject t)
		{
			DbContext.Set<TObject>().Add(t);
			await DbContext.SaveChangesAsync();
			return t;
		}

		public TObject Update(TObject updated, object key)
		{
			if (updated == null)
				return null;

			TObject existing = DbContext.Set<TObject>().Find(key);
			if (existing != null)
			{
				DbContext.Entry(existing).CurrentValues.SetValues(updated);
				DbContext.SaveChanges();
			}
			return existing;
		}

		public async Task<TObject> UpdateAsync(TObject updated, object key)
		{
			if (updated == null)
				return null;

			TObject existing = await DbContext.Set<TObject>().FindAsync(key);
			if (existing != null)
			{
				DbContext.Entry(existing).CurrentValues.SetValues(updated);
				await DbContext.SaveChangesAsync();
			}
			return existing;
		}

		public void Delete(TObject t)
		{
			DbContext.Set<TObject>().Remove(t);
			DbContext.SaveChanges();
		}

		public async Task<int> DeleteAsync(TObject t)
		{
			DbContext.Set<TObject>().Remove(t);
			return await DbContext.SaveChangesAsync();
		}

		public int Count()
		{
			return DbContext.Set<TObject>().Count();
		}

		public async Task<int> CountAsync()
		{
			return await DbContext.Set<TObject>().CountAsync();
		}
	}	
}

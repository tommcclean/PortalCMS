using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Portal.CMS.Repositories.Base
{
	public interface IRepositoryBase<TObject> where TObject : class
	{
		TObject Add(TObject t);
		Task<TObject> AddAsync(TObject t);
		int Count();
		Task<int> CountAsync();
		void Delete(TObject t);
		Task<int> DeleteAsync(TObject t);
		TObject Find(Expression<Func<TObject, bool>> match);
		ICollection<TObject> FindAll(Expression<Func<TObject, bool>> match);
		Task<ICollection<TObject>> FindAllAsync(Expression<Func<TObject, bool>> match);
		Task<TObject> FindAsync(Expression<Func<TObject, bool>> match);
		TObject Get(int id);
		ICollection<TObject> GetAll();
		Task<ICollection<TObject>> GetAllAsync();
		Task<TObject> GetAsync(int id);
		TObject Update(TObject updated, int key);
		Task<TObject> UpdateAsync(TObject updated, int key);
	}
}
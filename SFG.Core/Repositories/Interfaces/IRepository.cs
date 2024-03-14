using System.Linq.Expressions;
using SFG.Core.Domains.Shared;

namespace SFG.Core.Repositories.Interfaces
{
    public interface IRepository<T, TKey>
	{
        Task<int> AddEntity(T entity);
        Task<int> DeleteEntity(T entity);
        Task<int> UpdateEntity(T entity);
        Task<PageList<TResult>> GetPageListAsync<TResult>(Expression<Func<T, TResult>> selector, int page, int take);
        Task<PageList<TResult>> GetPageListAsync<TResult>(Expression<Func<T, bool>> conditions,Expression<Func<T, TResult>> selector, int page, int take, string[] properties = null);
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> conditions);
        Task<List<TResult>> GetAllAsync<TResult>(Expression<Func<T, bool>> conditions, Expression<Func<T, TResult>> selector);
        Task<T> GetAsyncById(Expression<Func<T, bool>> conditions);
        Task<TResult> GetAsyncById<TResult>(Expression<Func<T, bool>> conditions, Expression<Func<T, TResult>> selector);
    }
}


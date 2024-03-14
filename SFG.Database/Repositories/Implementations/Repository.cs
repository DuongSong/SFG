using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SFG.Core.Commons;
using SFG.Core.Domains.Shared;
using SFG.Core.Repositories.Interfaces;
using SFG.Database;

namespace SFG.Core.Repositories.Implementations
{
    public class Repository<T, TKey> : IRepository<T, TKey> where T : class
	{
        private readonly ApplicationDbContext dbContext;
        private readonly DbSet<T> table;
        private IQueryable<T> AllEntities { get; set; }

		public Repository(ApplicationDbContext dbContext)
		{
            this.dbContext = dbContext;
            table = dbContext.Set<T>();
            AllEntities = table.AsNoTracking();
		}

        public Task<int> AddEntity(T entity)
        {
            table.Add(entity);
            return dbContext.SaveChangesAsync();
        }

        public Task<int> UpdateEntity(T entity)
        {
            table.Update(entity);
            return dbContext.SaveChangesAsync();
        }

        public Task<int> DeleteEntity(T entity)
        {
            table.Remove(entity);
            return dbContext.SaveChangesAsync();
        }

        public Task<PageList<TResult>> GetPageListAsync<TResult>(Expression<Func<T, TResult>> selector, int page, int take)
        {
            var data = AllEntities.Select(selector);
            return data.ToPageListAsync(page, take);
        }

        public Task<PageList<TResult>> GetPageListAsync<TResult>(Expression<Func<T, bool>> conditions,
            Expression<Func<T, TResult>> selector, int page, int take, string[] properties = null)
        {
            var data = GetUsingInclude(properties).Where(conditions).Select(selector);
            return data.ToPageListAsync(page, take);
        }

        public Task<List<T>> GetAllAsync(Expression<Func<T, bool>> conditions)
        {
            return AllEntities.Where(conditions).ToListAsync();
        }

        public Task<List<TResult>> GetAllAsync<TResult>(Expression<Func<T, bool>> conditions, Expression<Func<T, TResult>> selector)
        {
            return AllEntities.Where(conditions).Select(selector).ToListAsync();
        }

        public Task<T> GetAsyncById(Expression<Func<T, bool>> conditions)
        {
            return AllEntities.Where(conditions).FirstOrDefaultAsync();
        }

        public Task<TResult> GetAsyncById<TResult>(Expression<Func<T, bool>> conditions, Expression<Func<T, TResult>> selector)
        {
            return AllEntities.Where(conditions).Select(selector).FirstOrDefaultAsync();
        }

        private IQueryable<T> GetUsingInclude(string[] properties = null)
        {
            var items = AllEntities;

            if(properties != null)
            {
                foreach(var property in properties)
                {
                    items = items.Include(property);
                }
            }
            return items;
        }
    }
}


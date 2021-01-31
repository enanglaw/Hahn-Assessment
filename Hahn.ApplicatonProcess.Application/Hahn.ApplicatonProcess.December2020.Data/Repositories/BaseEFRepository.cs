using Hahn.ApplicatonProcess.December2020.Data.Core;
using Hahn.ApplicatonProcess.December2020.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Data.Repositories
{
    public class BaseEFRepository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext _dbContext;
        public BaseEFRepository(AppDbContext context)
        {
            _dbContext = context;
        }

        public virtual async Task<T> AddItem(T tEntity)
        {
            try
            {
                await _dbContext.Set<T>().AddAsync(tEntity);
                return (await _dbContext.SaveChangesAsync()) > 0 ? tEntity : null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<T>> AddItems(IEnumerable<T> tEntities)
        {
            try
            {
                await _dbContext.Set<T>().AddRangeAsync(tEntities);
                return (await _dbContext.SaveChangesAsync()) > 0 ? tEntities : null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task<T> GetItem(int id)
        {
            try
            {
                return await _dbContext.Set<T>().FindAsync(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task<IEnumerable<T>> GetItems()
        {
            try
            {
                return await _dbContext.Set<T>().ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task<IEnumerable<T>> GetItems(Func<T, bool> predicate)
        {
            try
            {
                return await Task.Run(() => _dbContext.Set<T>().Where(predicate).ToList());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task<bool> RemoveItem(int id)
        {
            try
            {
                var item = await GetItem(id);
                if (item == null)
                    return false;
                _dbContext.Set<T>().Remove(item);
                return await _dbContext.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task<T> UpdateItem(int id, T updatedEntity)
        {
            try
            {
                var item = await GetItem(id);
                if (item == null)
                    throw new KeyNotFoundException($"Item with key {id} not found");
                _dbContext.Entry<T>(updatedEntity).State = EntityState.Modified;
                var updated = await _dbContext.SaveChangesAsync() == 1;
                return updated ? item : null;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

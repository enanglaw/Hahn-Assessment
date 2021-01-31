using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> AddItem(T tEntity);
        Task<IEnumerable<T>> AddItems(IEnumerable<T> tEntities);
        Task<T> GetItem(int id);
        Task<bool> RemoveItem(int id);
        Task<T> UpdateItem(int id, T updatedEntity);
        Task<IEnumerable<T>> GetItems();
        Task<IEnumerable<T>> GetItems(Func<T, bool> predicate);
    }
}

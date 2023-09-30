using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Core.Interfaces
{
    public interface IGenericRepository<T> where T : class 
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        IEnumerable<T> GetAll();

        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
        Task<T> GetByIdAsync(int Id , params Expression<Func<T, object>>[] includes);

        Task<T> GetAsync(int Id);
        Task AddAsync(T entity);
        Task updateAsync(int Id, T entity);
        Task DeleteAsync(int Id);

    }
}

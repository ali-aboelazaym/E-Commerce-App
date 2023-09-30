using Ecom.Core.Interfaces;
using Ecom.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Ecom.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity); 
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int Id)
        {
            var entity =await _context.Set<T>().FindAsync(Id);
            _context.Set<T>().Remove(entity);   
            await _context.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll()
        {
          return _context.Set<T>().AsNoTracking().ToList();
        }

        

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        public Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        public async Task<T> GetAsync(int Id)
        {
           return await _context.Set<T>().FindAsync(Id);   
           
        }

        public async Task<T> GetByIdAsync(int Id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query =_context.Set<T>();
            foreach (var item in includes)
            {
                query = query.Include(item);
            }
            return await ((DbSet<T>)query).FindAsync(Id);   
        }

        public async Task updateAsync(int Id, T entity)
        {
            var existingEntity = _context.Set<T>().FindAsync(Id);
            if (existingEntity != null)
            {
                _context.Update(existingEntity);
                await _context.SaveChangesAsync();
            }
        }
    }
}

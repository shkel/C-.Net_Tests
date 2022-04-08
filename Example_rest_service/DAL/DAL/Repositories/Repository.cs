using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL
{
    // TODO: own exceptions
    public class Repository<T> : IRepository<T> where T : class, IAutoKey
    {
        private DBContext _context;
        private DbSet<T> _dbSet;

        public Repository(DBContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("Context is null");
            }
            _context = context;
            _dbSet = context.Set<T>();
            if (_dbSet == null)
            {
                throw new ArgumentNullException("The set of Class is null");
            }
        }
        public T Create(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item", "Item is null");
            }
            var createdItem = _dbSet.Add(item);
            _context.SaveChanges();
            return createdItem.Entity;
        }
        public void Update(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("Item is null");
            }
            _context.Update(item);
            _context.SaveChanges();
        }

        public void Delete(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("Item is null");
            }
            _dbSet.Remove(item);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                return await _dbSet.AsNoTracking().ToListAsync();
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException("Argument is null", ex);
            }
        }

        public T GetByKey(int id)
        {
            var res = _dbSet.Find(id);
            _context.Entry(res).State = EntityState.Detached;
            return res;
        }
    }
}

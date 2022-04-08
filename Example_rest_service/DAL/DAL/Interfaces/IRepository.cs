using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        T GetByKey(int id);
        T Create(T item);
        void Update(T item);
        void Delete(T item);
    }
}

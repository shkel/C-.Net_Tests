using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ITrainingService<T>
    {
        T Create(T item);
        void Update(T item);
        void Delete(T item);
        T Get(int id);
        Task<IEnumerable<T>> GetAllAsync();
    }
}

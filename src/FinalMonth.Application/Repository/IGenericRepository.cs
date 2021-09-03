using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinalMonth.Application.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        IUnitOfWork UnitOfWOrk { get; }
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        void Update(T entity);
        void Create(T entity);
    }
} 
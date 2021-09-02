using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FinalMonth.Infrastructure.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        IUnitOfWork UnitOfWOrk { get; }
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
    }
} 
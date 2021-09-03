using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinalMonth.Application.Repository
{
    public interface IGenericQuery<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(string id);
    }
} 
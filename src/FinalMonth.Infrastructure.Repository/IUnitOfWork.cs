using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FinalMonth.Infrastructure.Repository
{
    public interface IUnitOfWork<T> where T : class
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
    }
}

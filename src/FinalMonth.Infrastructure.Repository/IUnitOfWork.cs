using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FinalMonth.Infrastructure.Repository
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}

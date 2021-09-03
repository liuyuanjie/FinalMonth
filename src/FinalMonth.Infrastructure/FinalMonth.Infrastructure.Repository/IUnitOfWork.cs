using System.Threading;
using System.Threading.Tasks;

namespace FinalMonth.Infrastructure.Repository
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}

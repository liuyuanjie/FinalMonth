using System.Threading;
using System.Threading.Tasks;
using FinalMonth.Application.Repository;

namespace FinalMonth.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FinalMonthIDbContext _iDbContext;

        public UnitOfWork(FinalMonthIDbContext iDbContext)
        {
            _iDbContext = iDbContext;
        }

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            return await _iDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}

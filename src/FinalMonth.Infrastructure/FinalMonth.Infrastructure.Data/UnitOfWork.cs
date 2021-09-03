using System.Threading;
using System.Threading.Tasks;
using FinalMonth.Application.Repository;

namespace FinalMonth.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FinalMonthDbContext _dbContext;

        public UnitOfWork(FinalMonthDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}

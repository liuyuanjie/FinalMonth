using System.Threading;
using System.Threading.Tasks;
using FinalMonth.Infrastructure.Repository;

namespace FinalMonth.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FinalMonthDataContext _dataContext;

        public UnitOfWork(FinalMonthDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _dataContext.SaveChangesAsync(cancellationToken);
        }
    }
}

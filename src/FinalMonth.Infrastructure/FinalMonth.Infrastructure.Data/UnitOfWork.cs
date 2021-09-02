using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FinalMonth.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace FinalMonth.Infrastructure.Data
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : class
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

        public async Task<List<T>> GetAllAsync()
        {
            return await _dataContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dataContext.Set<T>().FindAsync();
        }
    }
}

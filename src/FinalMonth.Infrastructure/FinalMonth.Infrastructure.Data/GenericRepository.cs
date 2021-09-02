using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FinalMonth.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FinalMonth.Infrastructure.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly FinalMonthDataContext _dataContext;

        public GenericRepository(FinalMonthDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IUnitOfWork UnitOfWork => _dataContext;

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

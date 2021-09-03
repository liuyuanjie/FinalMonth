using System.Collections.Generic;
using System.Threading.Tasks;
using FinalMonth.Application.Repository;
using Microsoft.EntityFrameworkCore;

namespace FinalMonth.Infrastructure.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly FinalMonthIDbContext _iDbContext;

        public GenericRepository(FinalMonthIDbContext iDbContext, IUnitOfWork unitOfWork)
        {
            _iDbContext = iDbContext;
            UnitOfWOrk = unitOfWork;
        }

        public IUnitOfWork UnitOfWOrk { get; }

        public async Task<List<T>> GetAllAsync()
        {
            return await _iDbContext.Set<T>().ToListAsync();
        }
        public async Task<T> GetByIdAsync(int id)
        {
            return await _iDbContext.Set<T>().FindAsync();
        }

        public void Update(T entity)
        {
            _iDbContext.Set<T>().Update(entity);
        }

        public void Create(T entity)
        { 
            _iDbContext.Set<T>().Add(entity);
        }
    }
}

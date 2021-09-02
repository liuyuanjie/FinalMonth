using System.Collections.Generic;
using System.Threading.Tasks;
using FinalMonth.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace FinalMonth.Infrastructure.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly FinalMonthDbContext _dbContext;

        public GenericRepository(FinalMonthDbContext dbContext, IUnitOfWork unitOfWork)
        {
            _dbContext = dbContext;
            UnitOfWOrk = unitOfWork;
        }

        public IUnitOfWork UnitOfWOrk { get; }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }
        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync();
        }

        public void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
        }

        public void Create(T entity)
        { 
            _dbContext.Set<T>().Add(entity);
        }
    }
}

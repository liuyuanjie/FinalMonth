using System.Collections.Generic;
using System.Threading.Tasks;
using FinalMonth.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace FinalMonth.Infrastructure.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly FinalMonthDataContext _dataContext;

        public GenericRepository(FinalMonthDataContext dataContext, IUnitOfWork unitOfWork)
        {
            _dataContext = dataContext;
            UnitOfWOrk = unitOfWork;
        }

        public IUnitOfWork UnitOfWOrk { get; }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dataContext.Set<T>().ToListAsync();
        }
        public async Task<T> GetByIdAsync(int id)
        {
            return await _dataContext.Set<T>().FindAsync();
        }

        public void Update(T entity)
        {
            _dataContext.Set<T>().Update(entity);
        }

        public void Create(T entity)
        { 
            _dataContext.Set<T>().Add(entity);
        }
    }
}

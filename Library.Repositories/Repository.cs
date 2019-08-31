using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Library.Models;
using Library.Repositories.Interfaces;

namespace Library.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly DataContext _dbContext;

        public Repository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }
        IQueryable<T> IRepository<T>.GetAll()
        {
            return _dbContext.Set<T>().AsNoTracking();
        }

        public async Task<T> GetById(int id)
        {
            return await _dbContext.Set<T>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task Add(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
        }

        public async Task<T> Update(int id, T entity)
        {
            _dbContext.Set<T>().Update(entity);
            return entity;
        }

        public async Task Delete(int id)
        {
            var entity = await GetById(id);
            _dbContext.Set<T>().Remove(entity);
        }
        public void Save()
        {
            _dbContext.SaveChangesAsync();
        }
    }
}

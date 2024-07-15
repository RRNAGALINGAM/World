using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using World.API.Data;
using World.API.Models;
using World.API.Repository.IRepository;

namespace World.API.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _DbContext;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _DbContext = dbContext;
        }
        public async Task Create(T enitity)
        {
            await _DbContext.AddAsync(enitity);
            await Save();
        }
        public async Task Delete(T enitity)
        {
            _DbContext.Remove(enitity);
            await Save();
        }
        public async Task<T> Get(int id)
        {
            return await _DbContext.Set<T>().FindAsync(id);
        }
        public async Task<List<T>> GetAll()
        {
            return await _DbContext.Set<T>().ToListAsync();
        }
        public bool IsRecordExists(Expression<Func<T, bool>> condition)
        {
            var result = _DbContext.Set<T>().AsQueryable().Where(condition).Any();
            return result;
        }
        public async Task Save()
        {
            await _DbContext.SaveChangesAsync();
        }
    }
}

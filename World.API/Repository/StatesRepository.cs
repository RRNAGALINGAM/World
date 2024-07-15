using Microsoft.EntityFrameworkCore;
using World.API.Data;
using World.API.Models;
using World.API.Repository.IRepository;


namespace World.API.Repository
{
    public class StatesRepository : GenericRepository<States>, IStatesRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public StatesRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Update(States entity)
        {
            _dbContext.States.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}

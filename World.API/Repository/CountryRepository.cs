using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using World.API.Data;
using World.API.Models;
using World.API.Repository.IRepository;

namespace World.API.Repository
{
    public class CountryRepository : GenericRepository<Country>, ICountryRepository
    {
        private readonly ApplicationDbContext _DbContext;

        public CountryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _DbContext = dbContext;
        }

        //public async Task Create(Country enitity)
        //{
        //    await _DbContext.Countries.AddAsync(enitity);
        //    await Save();
        //}

        //public async Task Delete(Country enitity)
        //{
        //    _DbContext.Countries.Remove(enitity);
        //    await Save();
        //}

        //public async Task<List<Country>> GetAll()
        //{
        //    List<Country> countries = await _DbContext.Countries.ToListAsync();
        //    return countries;

        //}

        //public async Task<Country> GetById(int id)
        //{
        //    Country country = await _DbContext.Countries.FindAsync(id);
        //    return country;
        //}

        //public bool IsCountryExists(string name)
        //{
        //    var result = _DbContext.Countries.AsQueryable().Where(x => x.Name.ToLower().Trim() == name.ToLower().Trim()).Any();
        //    return result;
        //}

        //public async Task Save()
        //{
        //    await _DbContext.SaveChangesAsync();
        //}

        public async Task Update(Country enitity)
        {
            _DbContext.Countries.Update(enitity);
            await _DbContext.SaveChangesAsync();
        }
    }
}

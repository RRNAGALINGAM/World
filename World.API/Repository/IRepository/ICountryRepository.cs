using World.API.Models;

namespace World.API.Repository.IRepository
{
    public interface ICountryRepository : IGenericRepository<Country>
    {
        //Task<List<Country>> GetAll();
        //Task<Country> GetById(int id);
        //Task Create (Country enitity);
        //Task Delete(Country enitity);
        //Task Save();
        //bool IsCountryExists(string name);
        Task Update (Country enitity);
    }
}

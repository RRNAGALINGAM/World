using System.Linq.Expressions;
using World.API.Models;

namespace World.API.Repository.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAll();

        Task<T> Get(int id);

        Task Create(T enitity);

        Task Delete(T enitity);

        Task Save();

        bool IsRecordExists(Expression<Func<T, bool>> conditon);

    }
}

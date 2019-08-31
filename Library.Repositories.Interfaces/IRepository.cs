using System.Linq;
using System.Threading.Tasks;
using Library.Models;

namespace Library.Repositories.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll();

        Task<T> GetById(int id);

        Task Add(T entity);

        Task<T> Update(int id, T entity);

        Task Delete(int id);
        
        void Save();
    }
}

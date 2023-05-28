using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExNhibernate.API.Repositories
{
    public interface IFuncionarioRepository<T> where T : class
    {
        Task Add(T item);
        Task Remove(long id);
        Task Update(T item);
        Task<T> FindById(long id);
        IEnumerable<T> FindAll();
    }
}

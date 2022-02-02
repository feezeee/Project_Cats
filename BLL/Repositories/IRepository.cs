using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();


        T Get(int id);
        Task<T> GetAsync(int id);


        IEnumerable<T> Find(Func<T, Boolean> predicate);
        Task<IEnumerable<T>> FindAsync(Func<T, Boolean> predicate);


        void Create(T entity);
        Task CreateAsync(T entity);


        void Update(T entity);
        Task UpdateAsync(T entity);


        void Delete(int id);
        Task DeleteAsync(int id);
    }
}

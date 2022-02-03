using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repository
{
    public interface IRepository<T> where T : class
    {
        // CRUD операции
        void Create(T entity);
        IQueryable<T> Get();
        void Update(T entity);
        void Delete(T entity);

        
    }
}

using BLL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {

        private CatContext db;

        public Repository(CatContext context)
        {
            this.db = context;
        }

        public void Create(T entity)
        {
            db.Set<T>().AddAsync(entity);           
        }

        public void Update(T entity)
        {
            db.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
       
        public IQueryable<T> Get()
        {
            return db.Set<T>().AsQueryable();
        }
        
        public void Delete(T entity)
        {   
            db.Set<T>().Remove(entity); 
        }

    }
}

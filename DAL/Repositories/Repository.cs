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

        public async Task CreateAsync(T entity)
        {
            if (entity != null)
            {
                await db.Set<T>().AddAsync(entity);
            }
        }

        public async Task UpdateAsync(T entity)
        {
            if (entity != null)
            {
                db.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }
        }
       
        public async Task<IQueryable<T>> GetAllAsync()
        {
            return await Task.Run(() => db.Set<T>());
        }

        public async Task DeleteAsync(T entity)
        {            
            if (entity != null)
            {
                db.Set<T>().Remove(entity);
            }
        }

    }
}

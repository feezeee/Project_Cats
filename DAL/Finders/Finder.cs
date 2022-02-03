using BLL.Finders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Finders
{
    public class Finder<T> : IFinder<T> where T : class
    {
        private CatContext db;
        public Finder(CatContext context)
        {
            db = context;
        }
        public async Task<IQueryable<T>> FindAsync(Func<T, bool> predicate)
        {
            return await Task.Run(() => db.Set<T>().Where(predicate).AsQueryable());
        }

        public async Task<T> GetAsync(int id)
        {
            return await db.Set<T>().FindAsync(id);
        }
    }
}

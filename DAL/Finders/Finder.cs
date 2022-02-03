using BLL.Finders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Finders
{
    public class Finder<T> where T : class
    {
        private CatContext db;
        public Finder(CatContext context)
        {
            db = context;
        }
        protected IQueryable<T> Find()
        {
            return db.Set<T>().AsQueryable();
        }
    }
}

using BLL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        CatContext db;
        public UnitOfWork(CatContext context)
        {
            db = context;
        }

        public Task<int> Save()
        {
           return db.SaveChangesAsync();
        }
    }
}

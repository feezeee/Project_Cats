using DAL.Data;
using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    internal class CatsRepository : IRepository<Cat>
    {
        private MyDB db;

        public CatsRepository(MyDB context)
        {
            this.db = context;
        }
        public void Create(Cat entity)
        {
            try
            {
                db.Cats.Add(entity);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при создании нового котика ))");
                Console.WriteLine(ex.Message);
            }
        }

        public void Delete(Cat entity)
        {
            try
            {
                db.Cats.Remove(entity);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Ошибка при удалении котика ((");
                Console.WriteLine(ex.Message);
            }
        }

        public IEnumerable<Cat> Find(Func<Cat, bool> predicate)
        {
            return db.Cats.Where(predicate).ToList();
        }

        public Cat Get(int id)
        {
            return db.Cats.Find(id);
        }

        public IEnumerable<Cat> GetAll()
        {
            return db.Cats;
        }

        public void Update(Cat entity)
        {
            db.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}

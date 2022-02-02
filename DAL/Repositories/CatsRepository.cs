using DAL.Data;
using DAL.Interfaces;
using DAL.Models;

namespace DAL.Repositories
{
    internal class CatsRepository : IRepository<CatDAL>
    {
        private MyDB db;

        public CatsRepository(MyDB context)
        {
            this.db = context;
        }
        public void Create(CatDAL entity)
        {
            db.Cats.Add(entity);
        }

        public void Delete(int id)
        {           
            var cat = db.Cats.Find(id);
            if(cat != null)
            {
                db.Cats.Remove(cat);
            }
        }

        public IEnumerable<CatDAL> Find(Func<CatDAL, bool> predicate)
        {
            return db.Cats.Where(predicate).ToList();
        }

        public CatDAL Get(int id)
        {
            return db.Cats.Find(id);
        }

        public IEnumerable<CatDAL> GetAll()
        {
            //Console.WriteLine("Выдаю котика");
            //foreach(var t in db.Cats)
            //{
            //    Console.WriteLine(t.Name);
            //}
            return db.Cats;
        }

        public void Update(CatDAL entity)
        {   
            var cat = db.Cats.Find(entity.Id);
            if (cat != null)
            {
                cat.Name = entity.Name;
                cat.Price = entity.Price;
                cat.DateOfBirth = entity.DateOfBirth;
                db.Entry(cat).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }
        }
    }
}

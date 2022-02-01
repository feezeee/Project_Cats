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
            return db.Cats;
        }

        public void Update(CatDAL entity)
        {            
            db.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}

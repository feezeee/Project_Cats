
namespace DAL.Repositories
{
    internal class CatsRepository : IRepository<Cat>
    {
        private CatContext db;

        public CatsRepository(CatContext context)
        {
            this.db = context;
        }
        public void Create(Cat entity)
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
            //Console.WriteLine("Выдаю котика");
            //foreach(var t in db.Cats)
            //{
            //    Console.WriteLine(t.Name);
            //}
            return db.Cats;
        }

        public void Update(Cat entity)
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

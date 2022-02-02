using BLL.Entities;
using BLL.Repository;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class CatsRepository : IRepository<Cat>
    {
        private CatContext db;

        public CatsRepository(CatContext context)
        {
            this.db = context;
        }
        public void Create(Cat entity)
        {
            if (entity != null)
            {
                entity.Id = 0;
                db.Cats.Add(entity);
                db.SaveChanges();
            }
        }

        public async Task CreateAsync(Cat entity)
        {
            //await Task.Run(() =>
            //{
                if (entity != null)
                {
                    entity.Id = 0;
                    await db.Cats.AddAsync(entity);
                    await db.SaveChangesAsync();
                }
            //});
        }

        public void Delete(int id)
        {           
            var cat = db.Cats.Find(id);
            if(cat != null)
            {
                db.Cats.Remove(cat);
                db.SaveChanges();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var cat = db.Cats.Find(id);
            if (cat != null)
            {
                db.Cats.Remove(cat);
                await db.SaveChangesAsync();
            }
        }

        public IEnumerable<Cat> Find(Func<Cat, bool> predicate)
        {
            return db.Cats.Where(predicate).ToList();
        }

        public async Task<IEnumerable<Cat>> FindAsync(Func<Cat, bool> predicate)
        {
            return await db.Cats.Where(predicate).AsQueryable().ToListAsync();
        }

        public Cat Get(int id)
        {
            return db.Cats.Find(id);
        }

        public async Task<Cat> GetAsync(int id)
        {
            return await db.Cats.FindAsync(id);
        }

        public IEnumerable<Cat> GetAll()
        {            
            return db.Cats;
        }

        public async Task<IEnumerable<Cat>> GetAllAsync()
        {
            return await db.Cats.ToListAsync();
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
                db.SaveChanges();
            }
        }

        public async Task UpdateAsync(Cat entity)
        {
            var cat = await GetAsync(entity.Id);
            if (cat != null)
            {
                cat.Name = entity.Name;
                cat.Price = entity.Price;
                cat.DateOfBirth = entity.DateOfBirth;
                db.Entry(cat).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await db.SaveChangesAsync();
            }
        }
    }
}

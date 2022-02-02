using BLL.Entities;
using BLL.Repository;


namespace BLL.Services
{
    public class CatService : ICatService
    {
        IRepository<Cat> cats { get; set; }

        public CatService(IRepository<Cat> cats)
        {
            this.cats = cats;
        }

        public void AddCat(Cat cat)
        {    
            if(cat != null)
            {
                cats.Create(cat);
            }
            
        }

        public async Task AddCatAsync(Cat cat)
        {
            if (cat != null)
            {
                await cats.CreateAsync(cat);
            }
        }


        public void DeleteCat(int id)
        {
            cats.Delete(id);
        }

        public async Task DeleteCatAsync(int id)
        {
            await cats.DeleteAsync(id);
        }


        public IEnumerable<Cat> GetCatBy(Func<Cat, bool> predicate)
        {
            return cats.Find(predicate);
        }

        public async Task<IEnumerable<Cat>> GetCatByAsync(Func<Cat, bool> predicate)
        {
            return await cats.FindAsync(predicate);
        }



        public IEnumerable<Cat> GetCats()
        {
            return cats.GetAll();
        }
        public async Task<IEnumerable<Cat>> GetCatsAsync()
        {
            return await cats.GetAllAsync();
        }


        public void UpdateCat(Cat cat)
        {
            if(cat != null)
            {
                cats.Update(cat);
            }           
        }
        public async Task UpdateCatAsync(Cat cat)
        {
            if (cat != null)
            {
                await cats.UpdateAsync(cat);
            }
        }


        public Cat FindCat(int id)
        {
            return cats.Get(id);
        }
        public async Task<Cat> FindCatAsync(int id)
        {
            return await cats.GetAsync(id);
        }







    }
}

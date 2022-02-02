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

        public void DeleteCat(int id)
        {
            cats.Delete(id);
        }


        public IEnumerable<Cat> GetCatBy(Func<Cat, bool> predicate)
        {
            return cats.Find(predicate);
        }

        public IEnumerable<Cat> GetCats()
        {
            return cats.GetAll();
        }

        public void UpdateCat(Cat cat)
        {
            if(cat != null)
            {
                cats.Update(cat);
            }           
        }

        public Cat? FindCat(int id)
        {
            return cats.Get(id);
        }
    }
}

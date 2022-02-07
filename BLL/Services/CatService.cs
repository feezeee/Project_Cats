using BLL.Entities;
using BLL.Finders;
using BLL.Repository;
using BLL.UnitOfWork;


namespace BLL.Services
{
    public class CatService : ICatService
    {
        IRepository<Cat> cats { get; set; }
        ICatFinder catFinder { get; set; }
        IUnitOfWork unitOfWork { get; set; }

        public CatService(IRepository<Cat> cats, ICatFinder catFinder, IUnitOfWork unitOfWork)
        {
            this.cats = cats;
            this.catFinder = catFinder;
            this.unitOfWork = unitOfWork;
        }       

        public void Create(Cat cat)
        {            
            cats.Create(cat);
            unitOfWork.Save();            
        }

        public IEnumerable<Cat> Get()
        {
            return cats.Get();
        }

        public void Update(Cat cat)
        {
            
            cats.Update(cat);
            unitOfWork.Save();
            
        }

        public void Delete(Cat cat)
        {     
           
            cats.Delete(cat);
            unitOfWork.Save();
            
        }

        public async Task<Cat> GetById(int id)
        {
            return await catFinder.GetById(id);
        }

        public async Task<IEnumerable<Cat>> GetByName(string name)
        {
            return await catFinder.GetByName(name);
        }

    }
}

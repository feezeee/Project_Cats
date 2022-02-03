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

        public async Task Create(Cat cat)
        {            
            cats.Create(cat);
            await unitOfWork.Save();            
        }

        public IEnumerable<Cat> Get()
        {
            return cats.Get();
        }

        public async Task Update(Cat cat)
        {
            var mycat = catFinder.GetById(cat.Id);
            if (mycat != null)
            {
                 mycat.Price = cat.Price;
                 mycat.Name = cat.Name;
                 mycat.DateOfBirth = cat.DateOfBirth;
                 cats.Update(mycat);
                 await unitOfWork.Save();
            }
        }

        public async Task Delete(Cat cat)
        {     
            var mycat = catFinder.GetById(cat.Id);
            if (mycat != null)
            {
                cats.Delete(mycat);
                await unitOfWork.Save();
            }   
        }

        public Cat GetById(int id)
        {
            return catFinder.GetById(id);
        }

        public async Task<IEnumerable<Cat>> GetByName(string name)
        {
            return await catFinder.GetByName(name);
        }

    }
}

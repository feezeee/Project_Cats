using BLL.Entities;
using BLL.Finders;
using BLL.Repository;
using BLL.UnitOfWork;

namespace BLL.Services
{
    public class CatService : ICatService
    {
        IRepository<Cat> cats { get; set; }
        IFinder<Cat> catFinder { get; set; }

        IUnitOfWork unitOfWork { get; set; }

        public CatService(IRepository<Cat> cats, IFinder<Cat> catFinder, IUnitOfWork unitOfWork)
        {
            this.cats = cats;
            this.catFinder = catFinder;
            this.unitOfWork = unitOfWork;
        }       

        public async Task AddCatAsync(Cat cat)
        {
            if (cat != null)
            {
                cat.Id = 0;
                await cats.CreateAsync(cat);
                await unitOfWork.SaveAsync();
            }
        }

        public async Task<IQueryable<Cat>> GetCatsAsync()
        {
            return await cats.GetAllAsync();
        }

        public async Task UpdateCatAsync(Cat cat)
        {
            if (cat != null)
            {
                var mycat = await catFinder.GetAsync(cat.Id);
                if (mycat != null)
                {
                    mycat.Price = cat.Price;
                    mycat.Name = cat.Name;
                    mycat.DateOfBirth = cat.DateOfBirth;
                    await cats.UpdateAsync(mycat);
                    await unitOfWork.SaveAsync();
                }
                
            }
        }

        public async Task DeleteCatAsync(Cat cat)
        {            
            if (cat != null)
            {
                var mycat = await catFinder.GetAsync(cat.Id);
                if (mycat != null)
                {
                    await cats.DeleteAsync(mycat);
                    await unitOfWork.SaveAsync();
                }
              
            }
            
        }

        public async Task<IQueryable<Cat>> GetCatByAsync(Func<Cat, bool> predicate)
        {
            return await catFinder.FindAsync(predicate);
        }

        public async Task<Cat> FindCatAsync(int id)
        {
            return await catFinder.GetAsync(id);
        }

    }
}

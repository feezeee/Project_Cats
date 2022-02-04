using BLL.Entities;
using BLL.Finders;
using BLL.Repository;
using BLL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AccountService : IAccountService
    {
        IRepository<Account> repository { get; set; }
        IAccountFinder accountFinder { get; set; }
        IUnitOfWork unitOfWork { get; set; }

        public AccountService(IRepository<Account> repository, IAccountFinder accountFinder, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.accountFinder = accountFinder;
            this.unitOfWork = unitOfWork;
        }

        public async Task Create(Account entity)
        {
            repository.Create(entity);
            await unitOfWork.Save();
        }

        public IEnumerable<Account> Get()
        {
            return repository.Get();
        }

        public async Task Update(Account entity)
        {            
                repository.Update(entity);
                await unitOfWork.Save();
            
        }

        public async Task Delete(Account entity)
        {
                repository.Delete(entity);
                await unitOfWork.Save();
            
        }

        public async Task<Account> GetByName(string name)
        {
            return await accountFinder.GetByName(name);
        }

        public async Task<Account> GetByNameAndPassword(string name, string password)
        {
            return await accountFinder.GetByNameAndPassword(name, password);
        }
    }
}

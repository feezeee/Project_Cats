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

        public void Create(Account entity)
        {
            repository.Create(entity);
            unitOfWork.Save();
        }

        public IEnumerable<Account> Get()
        {
            return repository.Get();
        }

        public void Update(Account entity)
        {            
            repository.Update(entity);
            unitOfWork.Save();            
        }

        public void Delete(Account entity)
        {
            repository.Delete(entity);
            unitOfWork.Save();            
        }

        public async Task<Account> GetByLogin(string name)
        {
            return await accountFinder.GetByLogin(name);
        }

        public async Task<Account> GetByLoginAndPassword(string name, string password)
        {
            return await accountFinder.GetByLoginAndPassword(name, password);
        }
    }
}

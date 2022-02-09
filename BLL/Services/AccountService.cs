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
        private readonly IRepository<Account> repository;
        private readonly IAccountFinder accountFinder;
        private readonly IUnitOfWork unitOfWork;
        private readonly IEncryption encryption;

        public AccountService(IRepository<Account> repository, IAccountFinder accountFinder, IUnitOfWork unitOfWork, IEncryption encryption)
        {
            this.repository = repository;
            this.accountFinder = accountFinder;
            this.unitOfWork = unitOfWork;
            this.encryption = encryption;
        }

        public Task Create(Account entity)
        {
            entity.Password = encryption.Encrypt(entity.Password);
            repository.Create(entity);
            return unitOfWork.Save();
        }

       
        public Task Update(Account entity)
        {
            entity.Password = encryption.Encrypt(entity.Password);
            repository.Update(entity);
            return unitOfWork.Save();            
        }

        public Task Delete(Account entity)
        {
            repository.Delete(entity);
            return unitOfWork.Save();            
        }
        public async Task<IEnumerable<Account>> Get()
        {
            return await accountFinder.Get();
        }


        public async Task<Account> GetByLogin(string name)
        {
            return await accountFinder.GetByLogin(name);
        }

        public async Task<Account> GetByLoginAndPassword(string name, string password)
        {
            password = encryption.Encrypt(password);
            return await accountFinder.GetByLoginAndPassword(name, password);
        }

        public async Task<Account> GetByRefreshToken(string refreshToken)
        {
            refreshToken = encryption.Encrypt(refreshToken);
            return await accountFinder.GetByRefreshToken(refreshToken);
        }

        //public async Task<Account> GetByIsActiveRefreshToken(string refreshToken)
        //{
            
        //    return await accountFinder.GetByRefreshToken(refreshToken);
        //}
    }
}

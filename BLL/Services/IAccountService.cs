using BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IAccountService
    {
        void Create(Account entity);
        IEnumerable<Account> Get();
        void Update(Account entity);
        void Delete(Account entity);
        Task<Account> GetByLogin(string login);
        Task<Account> GetByLoginAndPassword(string login, string password);
    }
}

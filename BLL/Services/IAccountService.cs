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
        Task Create(Account entity);
        IEnumerable<Account> Get();
        Task Update(Account entity);
        Task Delete(Account entity);
        Task<Account> GetByName(string name);
        Task<Account> GetByNameAndPassword(string name, string password);
    }
}

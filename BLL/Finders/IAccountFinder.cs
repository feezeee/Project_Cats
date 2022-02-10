using BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Finders
{
    public interface IAccountFinder
    {
        Task<List<Account>> Get();

        Task<Account?> GetByLogin(string login);
        Task<Account?> GetByLoginAndPassword(string login, string password);
        Task<Account?> GetByRefreshToken(string refreshToken);
        //Task<Account> GetByIsActiveRefreshToken(string refreshToken);
    }
}

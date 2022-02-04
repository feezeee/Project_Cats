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
        Task<Account> GetByName(string name);
        Task<Account> GetByNameAndPassword(string name, string password);

    }
}

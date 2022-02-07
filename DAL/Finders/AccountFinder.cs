using BLL.Entities;
using BLL.Finders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Finders
{
    public class AccountFinder : Finder<Account>, IAccountFinder
    {
        public AccountFinder(CatContext context) : base(context)
        {

        }


        public Task<Account> GetByLogin(string login)
        {
            return Find().FirstOrDefaultAsync(t=>t.Login == login);
        }

        public Task<Account> GetByLoginAndPassword(string login, string password)
        {
            return Find().FirstOrDefaultAsync(t => t.Login == login && t.Password == password);
        }
    }
}

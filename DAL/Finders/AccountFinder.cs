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


        public Task<Account> GetByName(string name)
        {
            return Find().FirstOrDefaultAsync(t=>t.Name == name);
        }

        public Task<Account> GetByNameAndPassword(string name, string password)
        {
            return Find().FirstOrDefaultAsync(t => t.Name == name && t.Password == password);
        }
    }
}

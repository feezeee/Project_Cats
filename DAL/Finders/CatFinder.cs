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
    public class CatFinder : Finder<Cat>, ICatFinder
    {
        public CatFinder(CatContext context) : base(context)
        {

        }

        public Cat GetById(int id)
        {
            return Find().First(t => t.Id == id);
        }

        public Task<List<Cat>> GetByName(string name)            
        {
            return Find().Where(t=>t.Name == name).ToListAsync();        
        }
    }
}

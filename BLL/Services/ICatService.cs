using BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface ICatService 
    {       
        Task AddCatAsync (Cat cat);
        Task<IQueryable<Cat>> GetCatsAsync();
        Task UpdateCatAsync (Cat cat);
        Task DeleteCatAsync (Cat cat);
        Task<Cat> FindCatAsync(int id);
        Task<IQueryable<Cat>> GetCatByAsync(Func<Cat, bool> predicate);
    }
}

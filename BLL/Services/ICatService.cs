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
        void AddCat(Cat cat);
        Task AddCatAsync (Cat cat);


        void UpdateCat(Cat cat);
        Task UpdateCatAsync (Cat cat);


        void DeleteCat(int id);
        Task DeleteCatAsync (int id);

        IEnumerable<Cat> GetCats();
        Task<IEnumerable<Cat>> GetCatsAsync();

        Cat FindCat(int id);
        Task<Cat> FindCatAsync(int id);


        IEnumerable<Cat> GetCatBy(Func<Cat, bool> predicate);
        Task<IEnumerable<Cat>> GetCatByAsync(Func<Cat, bool> predicate);
    }
}

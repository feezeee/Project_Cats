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
        void AddCat(Cat catBLL);
        void UpdateCat(Cat catBLL);
        void DeleteCat(int id);
        IEnumerable<Cat> GetCats();
        Cat FindCat(int id);
        IEnumerable<Cat> GetCatBy(Func<Cat, bool> predicate);

    }
}

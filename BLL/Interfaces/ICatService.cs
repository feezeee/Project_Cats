using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICatService
    {
        void AddCat(CatDTO catBLL);
        void UpdateCat(CatDTO catBLL);
        void DeleteCat(int id);
        IEnumerable<CatDTO> GetCats();
        CatDTO FindCat(int id);
        IEnumerable<CatDTO> GetCatBy(Func<CatDTO, bool> predicate);

    }
}

using BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Finders
{
    public interface ICatFinder
    {
        Task<List<Cat>> Get();

        Task<Cat?> GetById(int id);

        Task<List<Cat>> GetByName(string name);
    }
}

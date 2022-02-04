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
        Task Create (Cat cat);
        IEnumerable<Cat> Get();
        Task Update (Cat cat);
        Task Delete (Cat cat);
        Task<Cat> GetById(int id);
        Task<IEnumerable<Cat>> GetByName(string name);
    }
}

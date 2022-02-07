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
        void Create (Cat cat);
        IEnumerable<Cat> Get();
        void Update (Cat cat);
        void Delete (Cat cat);
        Task<Cat> GetById(int id);
        Task<IEnumerable<Cat>> GetByName(string name);
    }
}

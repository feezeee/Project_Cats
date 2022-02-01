using DAL.Data;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IService
    {
        IRepository<CatDAL> Cats { get; }
        void Save();

    }
}

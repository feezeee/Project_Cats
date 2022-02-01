using DAL.Data;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class EFService : IService
    {
        private MyDB db;
        private IRepository<CatDAL> catRepository;

        public EFService()
        {
            db = new MyDB();
        }
        public IRepository<CatDAL> Cats
        {
            get
            {
                if (catRepository == null)
                {
                    catRepository = new CatsRepository(db);
                }
                return catRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        
    }
}

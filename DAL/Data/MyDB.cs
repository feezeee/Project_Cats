using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data
{
    public class MyDB : DbContext
    {        
        internal DbSet<CatDAL> Cats { get; set; }

        public MyDB(DbContextOptions<MyDB> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlite("Data Source=D:\\helloapp.db");
        }

    }
}

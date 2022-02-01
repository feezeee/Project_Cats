using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DAL.Data
{
    public class MyDB : DbContext
    {        
        internal DbSet<CatDAL> Cats { get; set; }

        public MyDB() : base()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {           
            optionsBuilder.UseSqlServer("server=DESKTOP-DLRBUF2\\SQLEXPRESS;database=catsdb;Trusted_Connection=True;");
        }

    }
}

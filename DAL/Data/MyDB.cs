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
            //Database.EnsureCreated();   // создаем бд с новой схемой
        }
        public MyDB(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //server = DESKTOP - DLRBUF2\\SQLEXPRESS; database = catsdb; Trusted_Connection = True;
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=catsdb;Trusted_Connection=True;");
        }

    }
}

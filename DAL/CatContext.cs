using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class CatContext : DbContext
    {        
        internal DbSet<Cat> Cats { get; set; }

        public CatContext() : base()
        {
            //Database.EnsureCreated();   // создаем бд с новой схемой
        }
        public CatContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //server = DESKTOP - DLRBUF2\\SQLEXPRESS; database = catsdb; Trusted_Connection = True;
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=catsdb;Trusted_Connection=True;");
        }

    }
}

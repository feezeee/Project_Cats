using BLL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class CatContext : DbContext
    {
        internal DbSet<Cat> Cats { get; set; }
        internal DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().HasKey(u => new { u.Login });
        }

        public CatContext(DbContextOptions<CatContext> options) : base(options)
        {
            
        }

    }
}

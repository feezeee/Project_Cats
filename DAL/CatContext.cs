using BLL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class CatContext : DbContext
    {
        internal DbSet<Cat> Cats { get; set; }

        public CatContext(DbContextOptions<CatContext> options) : base(options)
        {
            
        }

    }
}

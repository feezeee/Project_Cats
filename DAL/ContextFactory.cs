using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class ContextFactory : IDesignTimeDbContextFactory<CatContext>
{
    public CatContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<CatContext>();

        string connectionString = "Server=(localdb)\\mssqllocaldb;Database=catsdb;Trusted_Connection=True;";

        if (args != null && args.Length > 0)
        {
            connectionString += args[0];
        }
        optionsBuilder.UseSqlServer(connectionString);
        return new CatContext(optionsBuilder.Options);
    }
}
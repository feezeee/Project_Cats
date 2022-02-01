using DAL.Data;
using DAL.Interfaces;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Ninject.Modules;

namespace BLL.Infrastructure
{
    public class ServiceModule : NinjectModule
    {
        private DbContextOptions<MyDB> options;
        public ServiceModule(DbContextOptions<MyDB> options)
        {
            this.options = options;
        }

        public override void Load()
        {
            Bind<IService>().To<EFService>().WithConstructorArgument(options);
        }
    }
}

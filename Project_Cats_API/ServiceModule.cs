using BLL.Interfaces;
using BLL.Services;
using DAL.Interfaces;
using Ninject.Modules;

namespace Project_Cats_API
{
    public class ServiceModule : NinjectModule
    {
        private readonly IService service;
        public ServiceModule(IService service)
        {
            this.service = service;
        }
        public override void Load()
        {
            Bind<IServiceManager>().To<ServiceManager>().WithConstructorArgument(service);
        }
    }
}

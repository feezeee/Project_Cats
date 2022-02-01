using BLL.Interfaces;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ServiceManager : IServiceManager
    {
        private ICatService catService;
        private readonly IService service;

        public ServiceManager(IService service)
        {
            this.service = service;
        }

        ICatService IServiceManager.catService
        {
            get
            {
                if (catService == null)
                {
                    catService = new CatService(service);
                }    
                return catService;
            }
        }

    }
}

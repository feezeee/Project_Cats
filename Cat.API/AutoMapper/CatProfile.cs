using AutoMapper;
using Cat.API.Models;

namespace Cat.API.AutoMapper
{
    public class CatProfile : Profile
    {
        public CatProfile()
        {
            CreateMap<BLL.Entities.Cat, CatModel>();
        }
    }
}

using AutoMapper;
using Cat.API.Request;

namespace Cat.API.AutoMapper
{
    public class CatProfile : Profile
    {
        public CatProfile()
        {
            CreateMap<PostCatRequest, BLL.Entities.Cat>();
            CreateMap<PutCatRequest, BLL.Entities.Cat>();
            CreateMap<DeleteCatRequest, BLL.Entities.Cat>();
        }
    }
}

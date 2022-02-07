using AutoMapper;
using Cat.API.Request;
using Cat.API.Response;

namespace Cat.API.AutoMapper
{
    public class CatProfile : Profile
    {
        public CatProfile()
        {
            CreateMap<PostCatRequest, BLL.Entities.Cat>();
            CreateMap<PutCatRequest, BLL.Entities.Cat>();
            CreateMap<DeleteCatRequest, BLL.Entities.Cat>();
            CreateMap<BLL.Entities.Cat, GetCatResponse>();
        }
    }
}

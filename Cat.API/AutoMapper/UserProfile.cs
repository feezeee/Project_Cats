using AutoMapper;
using Cat.API.Request;
using Cat.API.Response;

namespace Cat.API.AutoMapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<PostUserRequest, BLL.Entities.Account>();
            CreateMap<DeleteUserRequest, BLL.Entities.Account>();
            CreateMap<PutUserRequest, BLL.Entities.Account>();
            CreateMap<BLL.Entities.Account, GetAccountResponse>();
        }
    }
}

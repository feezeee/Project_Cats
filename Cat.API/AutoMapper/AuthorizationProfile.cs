using AutoMapper;
using BLL.Entities;
using Cat.API.Request;
using Cat.API.Response;

namespace Cat.API.AutoMapper
{
    public class AuthorizationProfile : Profile
    {
        public AuthorizationProfile()
        {
            CreateMap<Authorization, AuthorizationResponse>();
            CreateMap<PostAuthenticateRequest, Authentication>();
        }
    }
}

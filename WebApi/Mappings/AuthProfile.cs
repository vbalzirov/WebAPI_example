using AutoMapper;
using CompanyName.Application.Dal.Auth.Models;
using CompanyName.Application.Services.AuthService.Models;
using CompanyName.Application.WebApi.OrdersApi.Models.Auth.Requests;
using CompanyName.Application.WebApi.OrdersApi.Models.Auth.Responses;

namespace CompanyName.Application.WebApi.OrdersApi.Mappings
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<AuthResponse, AuthResult>();

            CreateMap<UserRegisterRequest, UserRegister>();

            CreateMap<UserRegister, UserDal>()
                .ForMember(src => src.UserName, opt => opt.MapFrom(x => x.UserName))
                .ForMember(src => src.Id, opt => opt.Ignore());

            CreateMap<AuthResult, AuthResponse>();

            CreateMap<UserLoginRequest, UserLogin>(); 
        }
    }
}
